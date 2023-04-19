using MapsterMapper;
using MediatR;


namespace CED.Application.Services.Abstractions.QueryHandlers;

public abstract class GetAllQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, List<TDto>>
    where TDto : class where TQuery : IRequest<List<TDto>>
{
    protected readonly IMapper _mapper;

    public GetAllQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<List<TDto>> Handle(TQuery query, CancellationToken cancellationToken);

}

