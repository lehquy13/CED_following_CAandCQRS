using CED.Contracts.Common.Models;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class RequestGettingClassDto : FullAuditedAggregateRootDto<Guid>
{
    public Guid TutorId { get; set; }
    public Guid ClassInformationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}
public class RequestGettingClassMinimalDto : EntityDto<Guid>
{
    public Guid TutorId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public Guid ClassInformationId { get; set; }

    public string TutorPhoneNumber { get; set; } = string.Empty;
    public string TutorEmail { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}
public class RequestGettingClassExtendDto : RequestGettingClassDto
{
    public string LearnerName { get; set; }= string.Empty;
    public string ContactNumber { get; set; }= string.Empty;
    
}