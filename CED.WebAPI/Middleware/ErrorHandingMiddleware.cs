using System.Net;
using System.Text.Json;

namespace CED.WebAPI.Middleware;

public class ErrorHandingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) { 
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request.", statusCode = 500 });

        context.Response.ContentType= "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}

