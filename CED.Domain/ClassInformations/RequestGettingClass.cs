using CED.Domain.Common.Models;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;

namespace CED.Domain.ClassInformations;

// TODO: Change RequestGettingClass to FullAggregateRoot when system is available
public class RequestGettingClass : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; } = null!;
    public Guid ClassInformationId { get; set; }
    public ClassInformation ClassInformation { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}