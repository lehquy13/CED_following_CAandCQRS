using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Queries.GetLearningClassesOfUserQuery;

public class GetLearningClassesOfUserQuery : GetObjectQuery<PaginatedList<ClassInformationForListDto>>
{
    
}