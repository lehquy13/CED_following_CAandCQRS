using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<AuthenticationResult>;

