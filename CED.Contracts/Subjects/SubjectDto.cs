using Abp.Application.Services.Dto;

namespace CED.Contracts.Subjects;

public class SubjectDto : FullAuditedEntityDto<Guid>
{

    public string Name { get; set; }
    public string Description { get; set; }

    public SubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
  
}

