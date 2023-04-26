using MediatR;
using Microsoft.AspNetCore.Http;

namespace CED.Application.Services.Authentication.Commands.SaveToken;

public record SaveTokenCommand
(
    string validateToken,
    HttpContext HttpContext
    ) : IRequest<bool>;

