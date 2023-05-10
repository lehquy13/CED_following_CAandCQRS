using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class UpdateTutorForClassCommand
    : IRequest<bool>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
    public Guid TutorId { get; set; }
}

