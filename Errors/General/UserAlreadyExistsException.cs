using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Errors.General
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string table, string email)
        : base($"{table} with {email} already exists.", StatusCodes.Status400BadRequest)
        {
        }
    }
}