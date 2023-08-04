using CED.Contracts.Users;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateUserCommand
    (
     UserDto UserDto ,
         string FilePath
): IRequest<Result<bool>>;

