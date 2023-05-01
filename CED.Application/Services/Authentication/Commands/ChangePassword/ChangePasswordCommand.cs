using MediatR;

namespace CED.Application.Services.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand
(
    Guid Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
    ) : IRequest<AuthenticationResult>;

