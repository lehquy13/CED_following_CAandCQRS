using MediatR;

namespace CED.Application.Services.UsersInformations.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    Guid TutorGuid,
    Guid ClassGuid
    ) : IRequest<bool>;

