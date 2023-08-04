using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Application.Services.ClassInformations.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQuery: GetObjectQuery<RequestGettingClassExtendDto>
{
    public Guid ClassInformationId { get; set; } = Guid.Empty;
}