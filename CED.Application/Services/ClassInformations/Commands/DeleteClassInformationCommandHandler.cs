using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using FluentResults;
using LazyCache;
using MediatR;
using Newtonsoft.Json;

namespace CED.Application.Services.ClassInformations.Commands;

public class DeleteClassInformationCommandHandler
    : DeleteCommandHandler<DeleteClassInformationCommand>
{
    
    private readonly IClassInformationRepository _classInformationRepository;
    public DeleteClassInformationCommandHandler(IClassInformationRepository classInformationRepository, IAppCache cache, IPublisher publisher, IUnitOfWork unitOfWork)
        :base(unitOfWork,cache,publisher)
    {
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<Result<bool>> Handle(DeleteClassInformationCommand command, CancellationToken cancellationToken)
    {
        //Check if the class existed
        ClassInformation? classInformation = await _classInformationRepository.GetById(command.Guid);
        if (classInformation is null)
        {
            return Result.Fail("Class doesn't exist"); 
        }
        classInformation.IsDeleted = true;
        if (await _unitOfWork.SaveChangesAsync() > 0)
        {
            //Remove the cache
            var defaultRequest = new GetAllClassInformationsQuery();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return Result.Ok(true)
                .WithSuccess(new Success("Delete class successfully"));
        }

        return Result.Fail("Fail to delete class"); 

    }
}

