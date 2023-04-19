using MediatR;

namespace CED.Application.Services.Users.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    Guid TutorGuid,
    Guid ClassGuid
    ) : IRequest<bool>;

