using MediatR;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    Guid TutorGuid,
    Guid ClassGuid
    ) : IRequest<bool>;

