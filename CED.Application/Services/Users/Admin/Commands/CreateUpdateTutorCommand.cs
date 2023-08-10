using CED.Contracts.Users.Tutors;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateTutorCommand
(
    TutorForDetailDto TutorForDetailDto,
    List<Guid> SubjectIds
) : IRequest<Result<bool>>;