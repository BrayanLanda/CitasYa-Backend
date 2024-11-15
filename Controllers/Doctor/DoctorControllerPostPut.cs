using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.DTOs;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CordiSimple.Controllers.Doctor
{
    public class DoctorControllerPostPut : DoctorControllerBase
    {
        public DoctorControllerPostPut(IDoctorRepository doctorRepository) : base(doctorRepository)
        {

        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update doctor",
            Description = "Updates the details of an existing doctor."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Tags("doctors")]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorUpdateDto doctorUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedGuest = await _doctorRepository.UpdateAsync(id, doctorUpdateDto);

            return Ok(updatedGuest);
        }
    }
}