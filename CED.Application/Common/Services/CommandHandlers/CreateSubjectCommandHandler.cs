using MapsterMapper;
using MediatR;

namespace CED.Application.Common.Services.CommandHandlers;

public abstract class CreateCommandHandler<TCommand>
    : IRequestHandler<TCommand, bool>
    where TCommand : IRequest<bool>
{
    protected readonly IMapper _mapper;

    public CreateCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<bool> Handle(TCommand request, CancellationToken cancellationToken);

}

