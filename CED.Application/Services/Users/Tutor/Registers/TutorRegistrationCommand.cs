using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Tutor.Registers;

public record TutorRegistrationCommand
(
    TutorDto TutorDto,
    List<string>? SubjectIds,
    List<string>? FilePaths

    ) : IRequest<bool>;

