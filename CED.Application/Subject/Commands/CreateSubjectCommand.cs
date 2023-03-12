using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class CreateSubjectCommand
    : IRequest<bool>
{
    public SubjectDto SubjectDto { get; set; } = null!;
}

