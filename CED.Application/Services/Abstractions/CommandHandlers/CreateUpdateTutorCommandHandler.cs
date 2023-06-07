using CED.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Abstractions.CommandHandlers;
// Not being used now
public abstract class CreateUpdateTutorCommandHandler<TCommand> : CreateUpdateCommandHandler<TCommand> where TCommand : IRequest<bool>
{
  
    
    public CreateUpdateTutorCommandHandler(ILogger<CreateUpdateCommandHandler<TCommand>> logger,IMapper mapper) : base(logger,mapper)
    {
     
    }

    public abstract override Task<bool> Handle(TCommand request, CancellationToken cancellationToken);

}

