using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.DTOs;
using CordiSimple.Errors.General;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using Microsoft.AspNetCore.Identity;

namespace CordiSimple.Services
{
    public class AuthService : IAuthRepository
    {
        private readonly IUserRespository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRespository userRespository, ITokenRepository tokenRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRespository;
            _tokenRepository = tokenRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserAuthResponseDto> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            //User Alreade Exits
            var existingUser = await _userRepository.GetUserByEmailAsync(userRegisterDto.Email);
            if(existingUser != null)
            {
                throw new UserAlreadyExistsException("User", userRegisterDto.Email);
            }

            //Add new User
            var newUser = new User
            {
                Name = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                Password = _passwordHasher.HashPassword(null, userRegisterDto.Password),
                Role = UserRole.USER
            };

            //Save user
            await _userRepository.AddUserAsync(newUser);

            //create token
            var token = _tokenRepository.CreateToken(newUser);

            //create and return
            return new UserAuthResponseDto
            {
                Token = token
            };
        }

        public async Task<UserAuthResponseDto> LoginAsync(UserLoginDto userLoginDto)
        {
            //Find user by email
            var user = await _userRepository.GetUserByEmailAsync(userLoginDto.Email);
            if(user == null)
            {
                throw new InvalidCredentialsException();
            }

            //Varify password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userLoginDto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            //Create token
            var token = _tokenRepository.CreateToken(user);

            //Create and return
            return new UserAuthResponseDto
            {
                Token = token
            };
        }
    }
}