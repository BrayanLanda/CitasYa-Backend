using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using Microsoft.IdentityModel.Tokens;

namespace CordiSimple.Services
{
    public class TokenServices(IConfiguration config) : ITokenRepository
    {
        public string CreateToken(User user)
        {
            var tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY")
                ?? throw new Exception("Cannot access tokenKey from appsettings");
            
            if(tokenKey.Length < 64) throw new Exception("Your need to be longer");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Name),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHanler = new JwtSecurityTokenHandler();
            var token = tokenHanler.CreateToken(tokenDescriptor);

            return tokenHanler.WriteToken(token);
        }
    }
}