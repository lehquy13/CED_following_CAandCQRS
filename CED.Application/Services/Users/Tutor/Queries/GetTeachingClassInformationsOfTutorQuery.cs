using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.Users.Tutor.Queries;

public class GetTeachingClassInformationsOfTutorQuery : IRequest<List<ClassInformationDto>>
{
    Guid Guid { get; set; }
}