using CED.Contracts.ClassInformations.Dtos;
using FluentResults;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public record CancelRequestGettingClassCommand(
    RequestGettingClassMinimalDto RequestGettingClassMinimalDto
) : IRequest<Result<bool>>;

    
