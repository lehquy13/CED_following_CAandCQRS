using CED.Contracts.Subjects;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQuery : IRequest<Result<SubjectDto>>
{
    public Guid Id { get; set; }
}