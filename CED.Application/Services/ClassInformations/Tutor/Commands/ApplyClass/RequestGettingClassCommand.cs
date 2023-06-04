using MediatR;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    string Email,
    Guid ClassGuid
    ) : IRequest<bool>;

