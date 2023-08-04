using CED.Contracts.Users;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Student.Commands;

public record LearnerInfoChangingCommand
(
    LearnerDto LearnerDto,
    string FilePath
    ) : IRequest<Result<bool>>;

