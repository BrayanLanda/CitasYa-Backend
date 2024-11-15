using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CordiSimple.Controllers.Auth
{
    [ApiController]
    [Route("api/v1/auth")]
    [Produces("application/json")]
    public abstract class AuthControllerBase : ControllerBase
    {
        protected readonly IAuthRepository _authRepository;

        public AuthControllerBase(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
    }
}