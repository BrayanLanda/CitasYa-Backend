using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Models;

namespace CordiSimple.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<Appointment> GetByIdAsync(int id);

        Task<IEnumerable<Appointment>> FilterByDateAsync(DateTime date);

        Task<IEnumerable<Appointment>> FilterBySpecialityAsync(string speciality);

        Task<IEnumerable<Appointment>> FilterByReasonAsync(string reason);

        Task<bool> IsDuplicateAsync(int doctorId, DateTime date);

        Task<Appointment> CreateAppointmentAsync(Appointment appointment);

        Task UpdateAsync(Appointment appointment);

        Task DeleteAsync(int id);

        Task AddNoteAsync(int appointmentId, string note);
    }
}