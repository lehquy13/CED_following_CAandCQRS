using CED.Domain.Common.Models;

namespace CED.Domain.ClassInformations;

public class AvailableDate : AuditedEntity<Guid>
{
    public DayOfWeek DayOfWeek { get; set; }
    public int BeginTime { get; set; }
    public int EndTime { get; set; }

}
