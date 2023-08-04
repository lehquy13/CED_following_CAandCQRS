using FluentValidation;

namespace CED.Application.Services.ClassInformations.Queries.GetClassInformation;

public class GetClassInformationQueryValidator : AbstractValidator<GetClassInformationQuery>
{
    public GetClassInformationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}