using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CordiSimple.Controllers.Appointment
{
    [ApiController]
    [Route("api/v1/appointment")]
    [Produces("application/json")]
    public class AppointmentControllerBase : ControllerBase
    {
        protected readonly IAppointmentRepository _appointmentRepository;

        public AppointmentControllerBase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
    }
}