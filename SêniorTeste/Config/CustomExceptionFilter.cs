using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SêniorTeste.API.Config
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new ContentResult
                {
                    Content = "Por favor, forneça um token válido.",
                    ContentType = "text/plain",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
