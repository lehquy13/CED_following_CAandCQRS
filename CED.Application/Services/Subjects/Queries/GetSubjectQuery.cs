using CED.Contracts.Subjects;
using MediatR;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQuery : IRequest<SubjectDto>
{
    public Guid id { get; set; }
}