using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUserCommand
    (
     UserDto UserDto 
): IRequest<bool>;

