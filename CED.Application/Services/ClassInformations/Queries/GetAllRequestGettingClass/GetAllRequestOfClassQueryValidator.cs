using FluentValidation;

namespace CED.Application.Services.ClassInformations.Queries.GetAllRequestGettingClass;

public class GetAllRequestOfClassQueryValidator : AbstractValidator<GetAllRequestOfClassQuery>
{
    public GetAllRequestOfClassQueryValidator()
    {
        RuleFor(x => x.ClassId).NotEmpty();
    }
}