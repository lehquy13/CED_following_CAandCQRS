using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Admin.Commands.ForgotPassword;

public record ForgotPasswordCommand
(
    string Email
    ) : IRequest<AuthenticationResult>;

