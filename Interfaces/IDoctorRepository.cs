using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.DTOs;
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
        
        Task<Doctor> UpdateAsync(int id, DoctorUpdateDto doctorUpdateDto);

        Task DeleteAsync(int id);
    }
}