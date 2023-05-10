using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Admin.Queries.Login;

public record LoginQuery
(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

