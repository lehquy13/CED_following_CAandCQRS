using CED.Domain.Common.Models;
using CED.Domain.Shared.NotificationConsts;

namespace CED.Domain.Notifications;

public class Notification : FullAuditedAggregateRoot<Guid>
{
    public string Message { get; set; } = string.Empty;
    public Guid ObjectId { get; set; }
    public bool IsRead { get; set; }
    public NotificationEnum NotificationType { get; set; } = NotificationEnum.Unknown;
}
