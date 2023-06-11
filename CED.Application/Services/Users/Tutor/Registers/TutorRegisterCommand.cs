using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Tutor.Registers;

public record TutorRegisterCommand
(
    TutorDto TutorDto
    ) : IRequest<bool>;

