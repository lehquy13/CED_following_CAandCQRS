using Abp.Domain.Entities.Auditing;
using System;
namespace CED.Domain.ClassInformations;

public class AvailableDate : AuditedAggregateRoot<Guid>
{
    public DayOfWeek DayOfWeek { get; set; }
    public int BeginTime { get; set; }
    public int EndTime { get; set; }

}
