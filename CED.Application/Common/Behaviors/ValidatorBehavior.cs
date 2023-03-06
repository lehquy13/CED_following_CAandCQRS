using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Common;
using FluentValidation;
using MediatR;

namespace CED.Application.Common.Behaviors;

public class ValidatorRegisterCommandBehavior :
    IPipelineBehavior<RegisterCommand, AuthenticationResult>
{
    private readonly IValidator<RegisterCommand> _validator;
    public ValidatorRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator= validator;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand request,
        RequestHandlerDelegate<AuthenticationResult> next,
        CancellationToken cancellationToken)
    {
        // before the handler

        var validationResult = await _validator.ValidateAsync(request);

        // after the handler


        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToList();
            throw new Exception(errors.FirstOrDefault()?.ErrorMessage);
        }




        return await next();
    }
}

