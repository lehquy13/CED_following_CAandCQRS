using CED.Contracts.Users.Tutors;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Tutor.Registers;

public record TutorRegistrationCommand
(
    TutorForDetailDto TutorForDetailDto,
    List<string>? SubjectIds,
    List<string>? FilePaths

    ) : IRequest<Result<bool>>;

