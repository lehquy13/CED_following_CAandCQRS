using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.ChangePassword;

public record ChangePasswordWhenForgotCommand
(
    Guid Id,
    string NewPassword
) : IRequest<bool>;

