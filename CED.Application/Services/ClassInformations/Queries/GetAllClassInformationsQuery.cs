using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQuery : IRequest<List<ClassInformationDto>>
{
    public Guid Guid { get; set; }   
}