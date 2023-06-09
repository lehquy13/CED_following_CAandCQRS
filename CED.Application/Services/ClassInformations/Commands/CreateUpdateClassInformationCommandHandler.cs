using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;


    public CreateUpdateClassInformationCommandHandler(
        IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        IUnitOfWork unitOfWork,
        IAppCache cache,
        ILogger<CreateUpdateClassInformationCommandHandler> logger, IMapper mapper)
        : base(logger, mapper)
    {
        _classInformationRepository = classInformationRepository;
        _unitOfWork = unitOfWork;
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _cache = cache;
    }

    public override async Task<bool> Handle(CreateUpdateClassInformationCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);

            //Check if the class existed
            if (classInformation is not null)
            {
                var classInformation1 = _mapper.Map<ClassInformation>(command.ClassInformationDto);
                classInformation1.LastModificationTime = DateTime.Now;
                if (classInformation1.TutorId != null)
                {
                    classInformation1.Status = Status.Confirmed;
                }
                var updatedEntity = _classInformationRepository.Update(classInformation1);
                if (updatedEntity != null )
                {
                    var requestGettingClasses = _requestGettingClassRepositoryepository.GetAll()
                        .Where(x => x.ClassInformationId.Equals(updatedEntity.Id))
                        .ToList();

                    foreach (var iClass in requestGettingClasses)
                    {
                        if (iClass.TutorId.Equals(updatedEntity.TutorId))
                        {
                            iClass.RequestStatus = RequestStatus.Success;
                        }
                        else
                        {
                            iClass.RequestStatus = RequestStatus.Canceled;
                        }
                    }

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
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