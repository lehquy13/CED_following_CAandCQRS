using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class DeleteSubjectCommand
    : IRequest<bool>
{
    public Guid id { get; set; }
}

