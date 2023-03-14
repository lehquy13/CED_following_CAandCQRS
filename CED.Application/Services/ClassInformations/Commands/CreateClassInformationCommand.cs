using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateClassInformationCommand
    : IRequest<bool>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
}

