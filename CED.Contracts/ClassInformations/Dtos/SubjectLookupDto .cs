using Abp.Application.Services.Dto;

namespace CED.Contracts.ClassInformations.Dtos;

public class SubjectLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
}

