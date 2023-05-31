using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Tutor.Queries;

public class GetTeachingClassInformationsOfTutorQuery : GetObjectQuery<PaginatedList<ClassInformationDto>>
{
    
}