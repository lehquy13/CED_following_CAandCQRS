using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Student.Commands;

public record StudentInfoChangingCommand
(
    UserDto StudentDto
    ) : IRequest<bool>;

