using MediatR;
using Microsoft.AspNetCore.Http;

namespace CED.Application.Services.Authentication.Commands.SaveToken;

//this currently fails the architecture
public class SaveTokenCommandHandler
    : IRequestHandler<SaveTokenCommand, bool>
{

    public SaveTokenCommandHandler()
    {
    }

    public async Task<bool> Handle(SaveTokenCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var token = command.validateToken;
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(1),
            //Domain = "yourdomain.com",
        };
        command.HttpContext.Response.Cookies.Append("access_token", token, cookieOptions);
        return true;
    }
}

