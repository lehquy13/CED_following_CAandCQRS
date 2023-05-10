using CED.Application.Services.Authentication.Admin.Queries.Login;
using FluentValidation;
namespace CED.Application.Services.Authentication.Queries.Login;

public class CustomerLoginQueryValidator : AbstractValidator<LoginQuery>
{
    public CustomerLoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

