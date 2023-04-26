using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public record DeleteSubjectCommand(
   Guid id
): IRequest<bool>;

