using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.DTOs
{
    public class DoctorRegisterDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Speciality is required.")]
        [StringLength(100, ErrorMessage = "Speciality cannot exceed 100 characters.")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
    }
}