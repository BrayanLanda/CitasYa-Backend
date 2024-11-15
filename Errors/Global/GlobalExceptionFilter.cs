using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Errors.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CordiSimple.Errors.Global
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "An unexpected error occurred.";

            switch (context.Exception)
            {
                case BaseException baseException:
                    statusCode = baseException.StatusCode;
                    message = baseException.Message;
                    break;
                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = validationException.Message;
                    break;
            }

            _logger.LogError(context.Exception, "An error occurred: {Message}", message);

            context.Result = new ObjectResult(new { error = message })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}