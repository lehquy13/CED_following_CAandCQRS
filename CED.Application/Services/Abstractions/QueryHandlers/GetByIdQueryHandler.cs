using MapsterMapper;
using MediatR;


namespace CED.Application.Services.Abstractions.QueryHandlers;

public abstract class GetByIdQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, TDto?>
    where TQuery : IRequest<TDto?>
{
    protected readonly IMapper _mapper;

    public GetByIdQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<TDto?> Handle(TQuery request, CancellationToken cancellationToken);

}

