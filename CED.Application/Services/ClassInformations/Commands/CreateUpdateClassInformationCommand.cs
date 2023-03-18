using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateUpdateClassInformationCommand
    : IRequest<bool>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
}

