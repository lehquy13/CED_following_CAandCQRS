using CED.Contracts.Subjects;
using MediatR;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQuery : IRequest<List<SubjectDto>>
{

}