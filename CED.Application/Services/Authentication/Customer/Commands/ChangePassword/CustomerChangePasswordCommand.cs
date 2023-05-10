using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.ChangePassword;

public record CustomerChangePasswordCommand
(
    Guid Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
    ) : IRequest<AuthenticationResult>;

