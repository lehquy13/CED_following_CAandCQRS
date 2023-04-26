using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public record ForgotPasswordCommand
(
    string Email
    ) : IRequest<AuthenticationResult>;

