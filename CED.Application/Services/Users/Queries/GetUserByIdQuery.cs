using CED.Domain.Shared.ClassInformationConsts;
using MediatR;

namespace CED.Application.Services.Users.Queries;

public class GetUserByIdQuery<TDto> : IRequest<TDto?> where TDto : class
{
    public Guid Id { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Undefined;
}