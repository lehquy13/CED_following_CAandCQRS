using CED.Application.Services.Authentication.Common;
using MediatR;
namespace CED.Application.Services.Authentication.Queries.Login;

public record LoginQuery
(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

