using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateUpdateClassInformationCommand
    : IRequest<bool>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
    public string email { get; set; } = null!;
}

