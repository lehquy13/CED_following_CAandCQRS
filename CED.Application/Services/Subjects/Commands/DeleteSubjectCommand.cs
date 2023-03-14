using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public class DeleteSubjectCommand
    : IRequest<bool>
{
    public Guid id { get; set; }
}

