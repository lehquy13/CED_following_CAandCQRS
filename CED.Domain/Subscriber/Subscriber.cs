using Abp.Domain.Entities;

namespace CED.Domain.Subscriber;
public class Subscriber : Entity<Guid>
{
    public Guid TutorId { get; set; }
}

