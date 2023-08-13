using System.Net;
using CED.Application.Services.Authentication.ValidateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CED.Web.Middleware;

public class AuthenticationMiddleware : IMiddleware
{
    private readonly ILogger<AuthenticationMiddleware> _logger;
    private readonly ISender _mediator;

    public AuthenticationMiddleware(ILogger<AuthenticationMiddleware> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var token = context.Session.GetString("access_token") ?? "";

            // if (token != "")
            //     context.token = token;
                
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
    private async Task AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var query = new ValidateTokenQuery(token);
       
            var result = await _mediator.Send(query);
            
            if(result is false)
                return;
            
            // var jwtToken = (JwtSecurityToken)validatedToken;
            // var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            //
            // // attach user to context on successful jwt validation
            // context.Items["User"] = userService.GetById(userId);
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}