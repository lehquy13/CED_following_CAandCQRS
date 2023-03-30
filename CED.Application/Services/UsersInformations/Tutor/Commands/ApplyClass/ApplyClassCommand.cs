using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Tutor.Commands.ApplyClass;

public record TutorInfoChangingCommand
(
    Guid TutorGuid,
    Guid ClassGuid
    ) : IRequest<bool>;

