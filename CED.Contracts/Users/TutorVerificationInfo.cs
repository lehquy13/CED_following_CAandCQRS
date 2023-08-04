using CED.Contracts.Common.Models;
using CED.Contracts.Models;

namespace CED.Contracts.Users;

public class TutorVerificationInfoDto : FullAuditedAggregateRootDto<Guid>
{
    public Guid TutorId { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}