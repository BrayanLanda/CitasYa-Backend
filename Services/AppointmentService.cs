using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Data;
using CordiSimple.Errors.General;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using Microsoft.EntityFrameworkCore;

namespace CordiSimple.Services
{
    public class AppointmentService : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todas las citas
    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.Include(a => a.User).Include(a => a.Doctor).ToListAsync();
    }

    // Obtener una cita por ID
    public async Task<Appointment> GetByIdAsync(int id)
    {
        var appointment = await _context.Appointments
                                        .Include(a => a.User)
                                        .Include(a => a.Doctor)
                                        .FirstOrDefaultAsync(a => a.Id == id);
        if (appointment == null)
        {
            throw new IdNotFoundException("Appointment", id); // Error específico
        }
        return appointment;
    }

    // Filtrar citas por fecha
    public async Task<IEnumerable<Appointment>> FilterByDateAsync(DateTime date)
    {
        return await _context.Appointments
                             .Where(a => a.Date.Date == date.Date)
                             .Include(a => a.User)
                             .Include(a => a.Doctor)
                             .ToListAsync();
    }

    // Filtrar citas por especialidad
    public async Task<IEnumerable<Appointment>> FilterBySpecialityAsync(string speciality)
    {
        return await _context.Appointments
                             .Where(a => a.Doctor.Speciality.Equals(speciality, StringComparison.OrdinalIgnoreCase))
                             .Include(a => a.User)
                             .Include(a => a.Doctor)
                             .ToListAsync();
    }

    // Filtrar citas por motivo
    public async Task<IEnumerable<Appointment>> FilterByReasonAsync(string reason)
    {
        return await _context.Appointments
                             .Where(a => a.Description.Contains(reason, StringComparison.OrdinalIgnoreCase))
                             .Include(a => a.User)
                             .Include(a => a.Doctor)
                             .ToListAsync();
    }

    // Validar si una cita ya existe para un doctor en un horario específico
    public async Task<bool> IsDuplicateAsync(int doctorId, DateTime date)
    {
        var existingAppointment = await _context.Appointments
                                                 .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.Date == date);
        if (existingAppointment != null)
        {
            throw new InvalidOperationException("Appointment already exists at this time."); // Error general
        }
        return true;
    }

    // Crear una nueva cita
    public async Task AddAsync(Appointment appointment)
    {
        // Validar disponibilidad antes de crear la cita
        await IsDuplicateAsync(appointment.DoctorId, appointment.Date);

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
    }

    // Actualizar una cita existente
    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    // Eliminar una cita
    public async Task DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            throw new IdNotFoundException("Appointment", id); // Error específico
        }
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    // Agregar una nota/comentario a una cita
    public async Task AddNoteAsync(int appointmentId, string note)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
        {
            throw new IdNotFoundException("Appointment", appointmentId); // Error específico
        }

        // Actualizar la descripción de la cita con la nota
        appointment.Description += $" - Note: {note}";
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
    }
}