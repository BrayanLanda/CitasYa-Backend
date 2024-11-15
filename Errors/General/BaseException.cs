using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Errors.General
{
    public class BaseException : Exception
    {
        public int StatusCode { get; }

        protected BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}