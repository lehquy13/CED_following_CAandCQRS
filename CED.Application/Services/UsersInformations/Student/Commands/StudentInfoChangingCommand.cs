using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.TutorRegister.Commands;

public record StudentInfoChangingCommand
(
    StudentDto StudentDto
    ) : IRequest<bool>;

