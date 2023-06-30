using CED.Contracts.Common.Models;

namespace CED.Contracts.Users;

public class TutorVerificationInfoDto : FullAuditedAggregateRootDto<Guid>
{
    public Guid TutorId { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}