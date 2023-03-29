using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Commands;

public record UserInfoChangingCommand
(
    UserDto UserDto
    ) : IRequest<bool>;

