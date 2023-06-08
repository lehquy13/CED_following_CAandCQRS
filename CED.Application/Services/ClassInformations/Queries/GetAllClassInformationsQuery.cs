using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQuery : GetObjectQuery<PaginatedList<ClassInformationDto>>
{
    public string SubjectName { get; set; } = string.Empty;
    public Status? Status { get; set; } 

    public GetAllClassInformationsQuery()
    {
        PageIndex = 1;
    }
    
}