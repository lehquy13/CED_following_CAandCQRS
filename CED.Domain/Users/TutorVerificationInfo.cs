
using CED.Domain.Common.Models;

namespace CED.Domain.Users;

public class TutorVerificationInfo : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}