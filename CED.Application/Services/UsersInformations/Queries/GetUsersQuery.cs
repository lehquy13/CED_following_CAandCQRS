using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MediatR;

namespace CED.Application.Services.UsersInformations.Queries;

public class GetUsersQuery<TDto> : IRequest<List<TDto>> where TDto : class
{
}