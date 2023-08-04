using CED.Application.Services.ClassInformations.Queries.GetAllRequestGettingClass;
using FluentValidation;

namespace CED.Application.Services.ClassInformations.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQueryValidator : AbstractValidator<GetTeachingClassDetailQuery>
{
    public GetTeachingClassDetailQueryValidator()
    {
        RuleFor(x => x.ClassInformationId).NotEmpty();
        RuleFor(x => x.ObjectId).NotEmpty();
    }
}