using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Models;

namespace CordiSimple.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(User user);
    }
}