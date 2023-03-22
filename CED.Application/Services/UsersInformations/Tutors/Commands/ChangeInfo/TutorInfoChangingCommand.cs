using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Tutors.Commands.ChangeInfo;

public record TutorInfoChangingCommand
(
    UserDto TutorDto
    ) : IRequest<bool>;

