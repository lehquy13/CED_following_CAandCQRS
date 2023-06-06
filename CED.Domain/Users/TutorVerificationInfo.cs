using Abp.Domain.Entities;

namespace CED.Domain.Users;

public class TutorVerificationInfo : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}