using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Admin.Commands.ChangePassword;

public record ChangePasswordCommand
(
    Guid Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
    ) : IRequest<AuthenticationResult>;

