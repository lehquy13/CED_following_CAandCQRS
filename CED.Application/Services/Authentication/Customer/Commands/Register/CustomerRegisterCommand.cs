using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.Register;

public record CustomerRegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<AuthenticationResult>;

