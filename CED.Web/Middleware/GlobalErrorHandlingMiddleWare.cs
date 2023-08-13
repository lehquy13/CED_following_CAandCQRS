using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CED.Web.Middleware;

public class GlobalErrorHandlingMiddleWare : IMiddleware
{
    private readonly ILogger _logger;
    public GlobalErrorHandlingMiddleWare(ILogger logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ProblemDetails problemDetails = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = ex.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            context.Response.ContentType = "application/json";
            

        }
    }
}