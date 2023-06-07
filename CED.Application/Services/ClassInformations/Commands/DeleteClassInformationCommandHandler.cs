using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Domain.ClassInformations;
using LazyCache;
using Newtonsoft.Json;

namespace CED.Application.Services.ClassInformations.Commands;

public class DeleteClassInformationCommandHandler
    : DeleteCommandHandler<DeleteClassInformationCommand>
{
    
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IAppCache _cache;
    public DeleteClassInformationCommandHandler(IClassInformationRepository classInformationRepository, IAppCache cache):base()
    {
        _classInformationRepository = classInformationRepository;
        _cache = cache;
    }
    public override async Task<bool> Handle(DeleteClassInformationCommand command, CancellationToken cancellationToken)
    {

        //Check if the class existed
        ClassInformation? classInformation = await _classInformationRepository.GetById(command.id);
        if (classInformation is null)
        {
            throw new Exception("Class has already existed");
        }

        classInformation.IsDeleted = true;
        
        var defaultRequest = new GetAllClassInformationsQuery();
        _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
        _classInformationRepository.Update(classInformation);

        return true;
    }
}

