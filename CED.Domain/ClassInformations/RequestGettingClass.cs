using Abp.Domain.Entities;

namespace CED.Domain.ClassInformations;

public class RequestGettingClass : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Guid ClassInformationId { get; set; }
}