using Abp.Domain.Entities;

namespace CED.Domain.Subscribers;
public class Subscriber : Entity<Guid>
{
    public Guid TutorId { get; set; }
}

