using CED.Contracts.ClassInformations.Dtos;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public record CancelRequestGettingClassCommand(
    RequestGettingClassMinimalDto RequestGettingClassMinimalDto
) : IRequest<bool>;

    
