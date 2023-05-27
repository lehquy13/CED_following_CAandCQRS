using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQuery : GetObjectQuery<List<ClassInformationDto>>
{
    public string SubjectName { get; set; } = string.Empty;
    
}