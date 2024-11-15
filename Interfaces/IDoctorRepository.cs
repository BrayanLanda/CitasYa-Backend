using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Models;

namespace CordiSimple.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(int id);

        Task<IEnumerable<Doctor>> GetBySpecialityAsync(string speciality);

        Task<bool> IsAvailableAsync(int doctorId, DateTime date);

        Task AddAsync(Doctor doctor);
        
        Task UpdateAsync(Doctor doctor);

        Task DeleteAsync(int id);
    }
}