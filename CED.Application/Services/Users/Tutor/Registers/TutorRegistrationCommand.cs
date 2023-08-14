using CED.Contracts.Users.Tutors;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Tutor.Registers;

public record TutorRegistrationCommand
(
    TutorForRegistrationDto TutorForRegistrationDto
    ) : IRequest<Result<bool>>;

