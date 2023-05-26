using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public class AddMajorCommand : IRequest<bool>
{
    public Guid TutorId { get; set; }
    public Guid SubjectId { get; set; }
}