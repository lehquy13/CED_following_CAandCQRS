using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class UpdateTutorForClassCommand
    : IRequest<bool>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
    public Guid TutorId { get; set; }
}

