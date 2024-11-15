using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required, StringLength(255)]
        public string Description { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }


        //Navegation Properties
        public User User { get; set; }
        public Doctor Doctor { get; set; }
    }
}