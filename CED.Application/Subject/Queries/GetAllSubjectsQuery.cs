using CED.Contracts.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.Login;

public class GetAllSubjectsQuery : IRequest<List<SubjectDto>>
{
   
}