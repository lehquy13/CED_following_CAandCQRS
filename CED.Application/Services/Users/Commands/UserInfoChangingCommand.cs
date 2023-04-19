using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Commands;

public record UserInfoChangingCommand
(
    UserDto UserDto
    ) : IRequest<bool>;

