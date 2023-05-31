using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQuery : GetObjectQuery<PaginatedList<ClassInformationDto>>
{
    public string SubjectName { get; set; } = string.Empty;

    public GetAllClassInformationsQuery()
    {
        PageIndex = 1;
    }
    
}