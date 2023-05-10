using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateUserCommand
    (
     UserDto UserDto 
): IRequest<bool>;

