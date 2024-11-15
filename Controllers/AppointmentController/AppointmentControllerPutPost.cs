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

namespace CordiSimple.Controllers.AppointmentController
{
    public class AppointmentControllerPutPost : AppointmentControllerBase
    {
        public AppointmentControllerPutPost(IAppointmentRepository appointmentRepository) : base(appointmentRepository)
        {

        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get appointment by ID",
            Description = "Retrieves an appointment by its unique ID."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Tags("appointments")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {

            var appointment = await _appointmentRepository.GetByIdAsync(id);
            return Ok(appointment);
        }

        [HttpPost]
        [SwaggerOperation(
    Summary = "Create a new appointment",
    Description = "Schedules a new appointment."
)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Tags("appointments")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = new Appointment
            {
                UserId = appointmentDto.UserId,
                DoctorId = appointmentDto.DoctorId,
                Description = appointmentDto.Description,
                Date = appointmentDto.Date,
                Status = appointmentDto.Status,
                Notes = appointmentDto.Notes
            };

            // Llamar al servicio para crear la cita
            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
        }
    }

    //     [HttpPost]
    //     [Authorize(Roles = "USER")]
    //     [SwaggerOperation(
    //        Summary = "Create a new appointment",
    //        Description = "Schedules a new appointment."
    //    )]
    //     [ProducesResponseType(StatusCodes.Status201Created)]
    //     [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //     [Tags("appointments")]
    //     public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto appointmentCreateDto)
    //     {
    //         if (!ModelState.IsValid)
    //         {
    //             return BadRequest(ModelState);
    //         }

    //         var appointment = new Appointment
    //         {
    //             UserId = appointmentCreateDto.UserId,
    //             DoctorId = appointmentCreateDto.DoctorId,
    //             Description = appointmentCreateDto.Description,

    //         }
    //     }
}
