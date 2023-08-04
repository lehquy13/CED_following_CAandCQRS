using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries.GetClassInformation;

public class GetClassInformationQuery : IRequest<ClassInformationDto>
{
    public Guid Id { get; set; }
}