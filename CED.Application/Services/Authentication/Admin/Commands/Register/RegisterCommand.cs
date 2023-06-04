using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Admin.Commands.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<AuthenticationResult>;

