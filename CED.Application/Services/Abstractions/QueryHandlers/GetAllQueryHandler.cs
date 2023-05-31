using CED.Contracts;
using MapsterMapper;
using MediatR;


namespace CED.Application.Services.Abstractions.QueryHandlers;

public abstract class GetAllQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, PaginatedList<TDto>>
    where TDto : class where TQuery : IRequest<PaginatedList<TDto>>
{
    protected readonly IMapper _mapper;

    public GetAllQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<PaginatedList<TDto>> Handle(TQuery query, CancellationToken cancellationToken);

}

