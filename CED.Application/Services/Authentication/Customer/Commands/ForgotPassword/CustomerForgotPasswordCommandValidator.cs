using CED.Application.Services.Authentication.Commands.Register;
using FluentValidation;

namespace CED.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public class CustomerForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public CustomerForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}

