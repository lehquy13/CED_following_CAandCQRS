﻿using FluentValidation;
namespace CED.Application.Services.Authentication.Admin.Queries.ValidateToken;

public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQuery>
{
    public ValidateTokenQueryValidator()
    {
        RuleFor(x => x.ValidateToken).NotEmpty();
        
    }
}
