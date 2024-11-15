using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.DTOs
{
    public class UserAuthResponseDto
    {
        [Required]
        public string Token { get; set; }

    }
}