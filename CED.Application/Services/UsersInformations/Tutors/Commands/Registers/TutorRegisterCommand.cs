using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Tutors.Commands.Registers;

public record TutorRegisterCommand
(
    TutorDto TutorDto
    ) : IRequest<bool>;

