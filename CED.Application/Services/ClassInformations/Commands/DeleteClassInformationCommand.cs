using FluentResults;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public record DeleteClassInformationCommand
(
    Guid Guid
) : IRequest<Result<bool>>;
