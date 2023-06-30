﻿using CED.Contracts.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CED.Contracts.Subjects;

public class CreateUpdateSubjectDto : FullAuditedAggregateRootDto<Guid>
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateUpdateSubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
}

