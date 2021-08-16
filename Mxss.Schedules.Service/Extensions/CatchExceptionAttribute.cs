using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mxss.Schedules.Service.Models.Responses;

namespace Mxss.Schedules.Service.Extensions
{
    public class CatchExceptionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private readonly string _errorCode;

        public CatchExceptionAttribute() { }

        public CatchExceptionAttribute(string errorCode)
        {
            _errorCode = errorCode;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var badResponse = new InvalidModelResponse(context.ModelState)
                {
                    Message = "Los datos enviados contienen errores"
                };
                context.Result = new BadRequestObjectResult(badResponse);
                base.OnActionExecuting(context);
            }
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                object response;
                Exception exception = context.Exception;
                context.ExceptionHandled = true;

                var exceptionType = exception.GetType();

                if (exceptionType == typeof(PersonsException))
                {
                    response = new ExceptionResponse()
                    {
                        Message = exception.Message,
                        Data = false,
                        Exception = null
                    };
                }
                else
                {
                    response = new ExceptionResponse()
                    {
                        Message = "Error desconocido, inteta de nuevo más tarde",
                        Data = false,
                        Exception = exception
                    };
                }

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}