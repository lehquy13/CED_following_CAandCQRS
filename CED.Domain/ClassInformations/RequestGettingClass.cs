using Abp.Domain.Entities;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.ClassInformations;

public class RequestGettingClass : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Guid ClassInformationId { get; set; }

    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}