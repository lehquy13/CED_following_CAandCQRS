using CED.Contracts.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.Login;

public class GetSubjectQuery : IRequest<SubjectDto>
{
   public Guid id { get; set; }
}