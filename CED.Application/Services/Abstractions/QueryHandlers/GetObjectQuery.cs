using FluentResults;
using MediatR;


namespace CED.Application.Services.Abstractions.QueryHandlers;

public class GetObjectQuery<TDto> : IRequest<Result<TDto>> where TDto : class
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    /// <summary>
    /// if(ObjectId == Guid.Empty) => GetAll
    /// </summary>
    public Guid ObjectId { get; set; }= Guid.Empty;
}



