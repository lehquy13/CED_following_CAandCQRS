﻿using Abp.Domain.Entities.Auditing;

namespace CED.Domain.Subjects;

public class Subject : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Subject()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}
