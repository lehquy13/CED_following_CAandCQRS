using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetTeachingClassInformationsOfTutorQuery : IRequest<List<ClassInformationDto>>
{
    Guid Guid { get; set; }
}