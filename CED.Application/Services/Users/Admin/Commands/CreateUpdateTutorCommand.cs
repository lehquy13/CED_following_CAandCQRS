using CED.Contracts.Users;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateTutorCommand
(
    TutorDto TutorDto,
    List<Guid> SubjectIds
) : IRequest<Result<bool>>;