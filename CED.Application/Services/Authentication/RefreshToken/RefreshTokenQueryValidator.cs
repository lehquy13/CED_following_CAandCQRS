﻿using FluentValidation;

namespace CED.Application.Services.Authentication.RefreshToken;

public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        
    }
}

