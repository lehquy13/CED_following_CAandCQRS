using FluentResults;
using MediatR;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    Guid TutorId,
    Guid ClassId
    ) : IRequest<Result<bool>>;

