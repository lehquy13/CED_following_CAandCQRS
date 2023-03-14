using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class DeleteClassInformationCommand
    : IRequest<bool>
{
    public Guid id { get; set; }
}

