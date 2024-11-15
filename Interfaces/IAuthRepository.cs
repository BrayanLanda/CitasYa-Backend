using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.DTOs;
using CordiSimple.Models;

namespace CordiSimple.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserAuthResponseDto> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<UserAuthResponseDto> LoginAsync(UserLoginDto userLoginDto);
    }
}