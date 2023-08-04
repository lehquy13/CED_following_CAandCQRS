using CED.Contracts.Common.Models;
using CED.Contracts.Models;
using CED.Domain.Shared.NotificationConsts;

namespace CED.Contracts.Notifications;

public class NotificationDto : FullAuditedAggregateRootDto<Guid>
{
    public string Message { get; set; } = string.Empty;
    public Guid ObjectId { get; set; }
    public string DetailPath { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public NotificationEnum NotificationType { get; set; } = NotificationEnum.Unknown;


}
