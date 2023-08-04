using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Queries.GetAllRequestGettingClass;

public class GetAllRequestOfClassQuery : GetObjectQuery<List<RequestGettingClassDto>>
{
    public Guid ClassId { get; set; } = Guid.Empty;
}