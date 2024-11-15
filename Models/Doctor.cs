using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public string Speciality { get; set; }

        public List<Appointment> Appointments { get; set; }

    }
}