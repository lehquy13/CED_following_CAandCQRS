
using CED.Domain.Common.Models;

namespace CED.Domain.Subjects;

public class TutorMajor : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Guid SubjectId { get; set; }
}