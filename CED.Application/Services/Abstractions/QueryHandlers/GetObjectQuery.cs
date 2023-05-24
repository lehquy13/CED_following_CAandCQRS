using MediatR;


namespace CED.Application.Services.Abstractions.QueryHandlers;

public class GetObjectQuery<TDto> : IRequest<TDto> where TDto : class
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;

    public Guid Guid { get; set; }= Guid.Empty;
}

