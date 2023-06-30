using CED.Contracts.Common.Models;

namespace CED.Contracts.Subjects;

public class SubjectDto : FullAuditedAggregateRootDto<Guid>
{

    public string Name { get; set; }
    public string Description { get; set; }

    public SubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
  
}

