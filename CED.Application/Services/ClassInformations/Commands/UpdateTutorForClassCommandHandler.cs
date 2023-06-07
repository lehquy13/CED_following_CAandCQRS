using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.ClassInformations;
using LazyCache;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Services.ClassInformations.Commands;

public class UpdateTutorForClassCommandHandler 
    : CreateUpdateCommandHandler<UpdateTutorForClassCommand>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IAppCache _cache;
    public UpdateTutorForClassCommandHandler(IClassInformationRepository classInformationRepository, IAppCache cache, ILogger<UpdateTutorForClassCommandHandler> logger, IMapper mapper)
        :base(logger,mapper)
    {
        _classInformationRepository = classInformationRepository;
        _cache = cache;
    }
    

    public override async Task<bool> Handle(UpdateTutorForClassCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);
            
            //Check if the class existed
            if (classInformation is  null)
            {
                throw new Exception("Class doesn't exist.");
            }
            command.ClassInformationDto.TutorDtoId = command.TutorId;
            var classInformation1 = _mapper.Map<ClassInformation>(command.ClassInformationDto);
            classInformation1.LastModificationTime = DateTime.Now;

            _classInformationRepository.Update(classInformation1);

            var defaultRequest = new CreateUpdateClassInformationCommand();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}

