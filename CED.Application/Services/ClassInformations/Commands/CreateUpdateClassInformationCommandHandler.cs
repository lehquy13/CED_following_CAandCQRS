using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Domain.ClassInformations;
using LazyCache;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateUpdateClassInformationCommandHandler 
    : CreateUpdateCommandHandler<CreateUpdateClassInformationCommand>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IAppCache _cache;


    public CreateUpdateClassInformationCommandHandler(IClassInformationRepository classInformationRepository,IAppCache cache, ILogger<CreateUpdateClassInformationCommandHandler> logger, IMapper mapper)
        :base(logger,mapper)
    {
        _classInformationRepository = classInformationRepository;
        _cache = cache;
    }

    public override async Task<bool> Handle(CreateUpdateClassInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);
            
            //Check if the class existed
            if (classInformation is not null)
            {
                var classInformation1 = _mapper.Map<ClassInformation>(command.ClassInformationDto);
                classInformation1.LastModificationTime = DateTime.Now;

                _classInformationRepository.Update(classInformation1);
               
            }
            else
            {
                classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);
                //classInformation = _mapper.From(command.ClassInformationDto).Adapt<ClassInformation>();
                await _classInformationRepository.Insert(classInformation);
            }

            var defaultRequest = new GetAllClassInformationsQuery();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when class is adding or updating." + ex.Message);
        }
    }
}

