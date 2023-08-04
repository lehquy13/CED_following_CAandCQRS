using CED.Contracts.ClassInformations.Dtos;
using FluentResults;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateUpdateClassInformationCommand
    : IRequest<Result<bool>>
{
    public ClassInformationDto ClassInformationDto { get; set; } = null!;
    public string Email { get; set; } = null!;
}

