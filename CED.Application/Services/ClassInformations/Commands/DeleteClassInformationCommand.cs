using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public record DeleteClassInformationCommand

(Guid id) : IRequest<bool>;
