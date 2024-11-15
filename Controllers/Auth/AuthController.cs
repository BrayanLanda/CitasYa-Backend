using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.DTOs;
using CordiSimple.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CordiSimple.Controllers.Auth
{
    public class AuthController : AuthControllerBase
    {
        public AuthController(IAuthRepository authRepository) : base(authRepository)
        {
            
        }

        [HttpPost("register")]
        [Tags("auth")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.RegisterAsync(userRegisterDto);

            return CreatedAtAction(nameof(Register), new { token = result.Token }, result);
        }

        [HttpPost("login")]
        [SwaggerOperation(
        Summary = "Login an existing user",
        Description = "Authenticates a user by verifying the provided credentials."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Tags("auth")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.LoginAsync(userLoginDto);

            return Ok(result);
        }
    }
}