using MediatR;

namespace CED.Application.Services.UsersInformations.Commands;

public record DeleteUserCommand
(
    Guid guid
    ) : IRequest<bool>;

