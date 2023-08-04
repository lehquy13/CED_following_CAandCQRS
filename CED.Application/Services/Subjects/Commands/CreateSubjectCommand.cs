using CED.Contracts.Subjects;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUpdateSubjectCommand
    : IRequest<Result<bool>>
{
    public SubjectDto SubjectDto { get; set; } = null!;
}

