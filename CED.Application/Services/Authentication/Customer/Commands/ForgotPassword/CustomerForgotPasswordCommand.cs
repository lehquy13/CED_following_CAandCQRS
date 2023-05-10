using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public record CustomerForgotPasswordCommand
(
    string Email
    ) : IRequest<AuthenticationResult>;

