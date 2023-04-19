using MediatR;

namespace CED.Application.Services.Abstractions.CommandHandlers;

public abstract class DeleteCommandHandler<TCommand>
    : IRequestHandler<TCommand, bool>
    where TCommand : IRequest<bool>
{
    public DeleteCommandHandler() { }

    public abstract Task<bool> Handle(TCommand command, CancellationToken cancellationToken);
}

