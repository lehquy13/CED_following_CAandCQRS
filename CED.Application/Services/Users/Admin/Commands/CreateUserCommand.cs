using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUserCommand
    : IRequest<bool>
{
    public UserDto UserDto { get; set; } = null!;
}

