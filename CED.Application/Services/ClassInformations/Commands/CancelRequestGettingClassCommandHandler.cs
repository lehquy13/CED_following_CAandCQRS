using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.ClassInformations.Commands;

public class CancelRequestGettingClassCommandHandler : CreateUpdateCommandHandler<CancelRequestGettingClassCommand>
{
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepository;

    public CancelRequestGettingClassCommandHandler(
        ILogger<CancelRequestGettingClassCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IPublisher publisher,
        IAppCache cache,
        IMapper mapper,
        IRepository<RequestGettingClass> requestGettingClassRepository
    ) : base(logger, mapper,unitOfWork, cache,publisher)
    {
        _requestGettingClassRepository = requestGettingClassRepository;
    }

    public override async Task<Result<bool>> Handle(CancelRequestGettingClassCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var requestGettingClassFromDb = await _requestGettingClassRepository.GetById(command.RequestGettingClassMinimalDto.Id);
        if (requestGettingClassFromDb is not null)
        {
            requestGettingClassFromDb = _mapper.Map<RequestGettingClass>(command.RequestGettingClassMinimalDto);
        }

        if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
        {
            return true;
        }
        
        return false;
    }
}