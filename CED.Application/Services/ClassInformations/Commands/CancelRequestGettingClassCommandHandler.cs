using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CancelRequestGettingClassCommandHandler : IRequestHandler<CancelRequestGettingClassCommand, bool>
{
    private readonly IMapper _mapper ;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;

    public CancelRequestGettingClassCommandHandler(
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        IMapper mapper)
    {
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CancelRequestGettingClassCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var request = _mapper.Map<RequestGettingClass>(command.RequestGettingClassMinimalDto);
        var update = _requestGettingClassRepositoryepository.Update(request);
        if (update != null) return true;
        return false;
    }
}

    
