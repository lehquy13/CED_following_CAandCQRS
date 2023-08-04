using CED.Application.Services.ClassInformations.Queries.GetClassInformation;
using FluentValidation;

namespace CED.Application.Services.ClassInformations.Queries.GetAllRequestGettingClass;

public class GetAllRequestGettingClassQueryValidator : AbstractValidator<GetAllRequestOfClassQuery>
{
    public GetAllRequestGettingClassQueryValidator()
    {
        RuleFor(x => x.ClassId).NotEmpty();
    }
}