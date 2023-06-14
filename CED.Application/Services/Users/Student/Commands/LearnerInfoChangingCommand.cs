using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Student.Commands;

public record LearnerInfoChangingCommand
(
    LearnerDto LearnerDto,
    string FilePath
    ) : IRequest<bool>;

