
using CED.Domain.Common.Models;

namespace CED.Domain.Subscribers;
public class Subscriber : Entity<Guid>
{
    public Guid TutorId { get; set; }
}

