using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateTutorCommand
(
    UserDto UserDto,
    List<Guid> SubjectId
) : IRequest<bool>;