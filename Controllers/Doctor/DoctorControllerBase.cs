using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CordiSimple.Controllers.Doctor
{
    [ApiController]
    [Route("api/v1/doctor")]
    [Produces("application/json")]
    public class DoctorControllerBase : ControllerBase
    {
        protected readonly IDoctorRepository _doctorRepository;

        public DoctorControllerBase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
    }
}