using FluentResults;
using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public record DeleteSubjectCommand(
   Guid SubjectId
): IRequest<Result<bool>>;

