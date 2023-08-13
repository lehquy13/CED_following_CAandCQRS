using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CED.Web.Middleware
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //base.OnException(context);

            var exception = context.Exception;
            var problemDetail = new ProblemDetails
            {
                Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1",
                //Title = "An error occurred while processing your request.",
                Title = exception.Message,
                //Status = (int)HttpStatusCode.InternalServerError
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Result = new ObjectResult(problemDetail);
           
            context.ExceptionHandled = true;


        }
    }
}
