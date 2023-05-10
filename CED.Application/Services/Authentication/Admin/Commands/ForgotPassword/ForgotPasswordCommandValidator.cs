using FluentValidation;
namespace CED.Application.Services.Authentication.Commands.Register;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}

