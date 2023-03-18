using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQuery : IRequest<List<ClassInformationDto>>
{

}