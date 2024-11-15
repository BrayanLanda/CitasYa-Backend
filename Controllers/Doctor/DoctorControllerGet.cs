using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CordiSimple.Controllers.Doctor
{
    public class DoctorControllerGet : DoctorControllerBase
    {
        public DoctorControllerGet(IDoctorRepository doctorRepository) : base(doctorRepository)
        {

        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Get all doctors",
           Description = "Retrieves all doctors from the database."
       )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Tags("doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get doctor by ID",
            Description = "Retrieves a doctor by their unique ID."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Tags("doctors")]
        public async Task<IActionResult> GetDoctorById(int id)
        {

            var doctor = await _doctorRepository.GetByIdAsync(id);
            return Ok(doctor);
        }

        [HttpGet("search/{keyword}")]
        [Authorize(Roles = "USER")]
        [SwaggerOperation(
        Summary = "Search guests",
        Description = "Searches for doctor in the database using a keyword. Requires ADMIN role."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Tags("doctors")]
        public async Task<IActionResult> SearchDoctor(string keyword)
        {
            var doctors = await _doctorRepository.GetBySpecialityAsync(keyword);
            return Ok(doctors);
        }
    }
}