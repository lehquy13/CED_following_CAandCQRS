using CED.Application.Services.Authentication.Commands.Register;
using FluentValidation;

namespace CED.Application.Services.Authentication.Customer.Commands.Register;

public class CustomerRegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public CustomerRegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

