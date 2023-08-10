using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;

public class GetAllClassInformationsQuery : GetObjectQuery<PaginatedList<ClassInformationForListDto>>
{
    public string SubjectName { get; set; } = string.Empty;
      public Status? Status { get; set; }
  
      public string Filter = string.Empty;
      public GetAllClassInformationsQuery()
      {
          PageIndex = 1;
      }
    
}