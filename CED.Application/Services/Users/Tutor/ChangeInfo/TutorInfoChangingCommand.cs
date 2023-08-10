using CED.Contracts.Users.Tutors;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Tutor.ChangeInfo;

public record TutorInfoChangingCommand
(
    TutorMainInfoDto TutorDto,
    List<Guid> SubjectIds,
    List<string> FilePaths
    ) : IRequest<Result<bool>>;

