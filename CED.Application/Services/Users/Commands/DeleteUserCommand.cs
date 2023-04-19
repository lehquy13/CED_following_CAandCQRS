using MediatR;

namespace CED.Application.Services.Users.Commands;

public record DeleteUserCommand
(
    Guid guid
    ) : IRequest<bool>;

