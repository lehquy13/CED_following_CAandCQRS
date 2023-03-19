using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public record TutorRegisterCommand
(
    TutorDto TutorDto
    ) : IRequest<bool>;

