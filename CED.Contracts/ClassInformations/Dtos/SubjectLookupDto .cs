using Abp.Application.Services.Dto;
using System;

namespace CED.Contracts.ClassInformations;

public class SubjectLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
}

