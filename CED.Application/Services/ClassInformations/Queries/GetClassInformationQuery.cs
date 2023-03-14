using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationQuery : IRequest<ClassInformationDto>
{
    public Guid id { get; set; }
}