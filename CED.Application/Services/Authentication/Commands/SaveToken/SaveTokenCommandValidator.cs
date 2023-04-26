using FluentValidation;
namespace CED.Application.Services.Authentication.Commands.SaveToken;

public class SaveTokenCommandValidator : AbstractValidator<SaveTokenCommand>
{
    public SaveTokenCommandValidator()
    {
        RuleFor(x => x.validateToken).NotEmpty();
        RuleFor(x => x.HttpContext).NotEmpty();
    }
}

