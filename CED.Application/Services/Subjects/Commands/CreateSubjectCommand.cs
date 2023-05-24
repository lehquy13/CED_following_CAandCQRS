using CED.Contracts.Subjects;
using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUpdateSubjectCommand
    : IRequest<bool>
{
    public SubjectDto? SubjectDto { get; set; } = null!;
}

