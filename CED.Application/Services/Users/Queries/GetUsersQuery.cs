using MediatR;

namespace CED.Application.Services.Users.Queries;

public record GetUsersQuery<TDto> : IRequest<List<TDto>> where TDto : class;
