using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Tutor.Commands.ChangeInfo;

public record TutorInfoChangingCommand
(
    UserDto TutorDto
    ) : IRequest<bool>;

