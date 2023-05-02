using MediatR;

namespace CED.Application.Services.Users.Queries;

public class GetUsersQuery<TDto> : IRequest<List<TDto>> where TDto : class
{
}