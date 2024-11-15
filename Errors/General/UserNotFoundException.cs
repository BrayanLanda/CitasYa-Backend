using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Errors.General
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string model, string email)
            : base($"{model} with {email} was not found.", StatusCodes.Status404NotFound)
        {
        }
    }
}