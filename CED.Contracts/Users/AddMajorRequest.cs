namespace CED.Contracts.Users;

public class AddMajorRequest
{
    public Guid TutorId { get; set; }
    public List<Guid> SubjectId { get; set; } = new();
}