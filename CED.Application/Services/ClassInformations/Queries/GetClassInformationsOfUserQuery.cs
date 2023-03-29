using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationsOfUserQuery : IRequest<List<ClassInformationDto>>
{
    public Guid Guid { get; set; }
}