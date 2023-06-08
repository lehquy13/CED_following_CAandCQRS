using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Abstractions.CommandHandlers;

public abstract class CreateUpdateCommandHandler<TCommand>
    : IRequestHandler<TCommand, bool>
    where TCommand : IRequest<bool>
{
    protected readonly IMapper _mapper;
    protected readonly ILogger<CreateUpdateCommandHandler<TCommand>> _logger;

    public CreateUpdateCommandHandler(ILogger<CreateUpdateCommandHandler<TCommand>> logger,IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public abstract Task<bool> Handle(TCommand request, CancellationToken cancellationToken);

}
public abstract class NewCreateUpdateCommandHandler<TCommand>
    : IRequestHandler<TCommand, Result<bool>>
    where TCommand : IRequest<Result<bool>>
{
    protected readonly IMapper _mapper;
    protected readonly ILogger<NewCreateUpdateCommandHandler<TCommand>> _logger;

    public NewCreateUpdateCommandHandler(ILogger<NewCreateUpdateCommandHandler<TCommand>> logger,IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public abstract Task<Result<bool>> Handle(TCommand request, CancellationToken cancellationToken);

}

