using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Data;
using CordiSimple.DTOs;
using CordiSimple.Errors.General;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using Microsoft.EntityFrameworkCore;

namespace CordiSimple.Services
{
    public class DoctorService : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los doctores
        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        // Obtener un doctor por ID
        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new IdNotFoundException("Doctor", id); // Error específico
            }
            return doctor;
        }

        // Validar la disponibilidad de un doctor en una fecha/hora específica
        public async Task<bool> IsAvailableAsync(int doctorId, DateTime date)
        {
            var appointment = await _context.Appointments
                                             .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.Date == date);
            if (appointment != null)
            {
                throw new InvalidOperationException("Doctor is not available at this time."); // Error general
            }
            return true;
        }

        public async Task<IEnumerable<Doctor>> GetBySpecialityAsync(string speciality)
        {
            return await _context.Doctors
                .Where(g => g.Name.Contains(speciality)|| g.Speciality.Contains(speciality))
                .ToListAsync();
        }

        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

       public async Task<Doctor> UpdateAsync(int id, DoctorUpdateDto doctorUpdateDto)
        {
            var existingGuest = await _context.Doctors.FindAsync(id);
            if (existingGuest == null)
            {
                throw new IdNotFoundException("Doctor", id);
            }

            // Actualizar solo los campos necesarios
            existingGuest.Name = doctorUpdateDto.Name;
            existingGuest.Email = doctorUpdateDto.Email;

             _context.Doctors.Update(existingGuest);
            await _context.SaveChangesAsync();
            return existingGuest;
        }
        // Eliminar un doctor
        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new IdNotFoundException("Doctor", id); // Error específico
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}