using FluentValidation;
namespace CED.Application.Services.Authentication.Queries.ValidateToken;

public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQuery>
{
    public ValidateTokenQueryValidator()
    {
        RuleFor(x => x.validateToken).NotEmpty();
        
    }
}

