using CED.Domain.ClassInformations;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateClassInformationCommandHandler
    : IRequestHandler<CreateUpdateClassInformationCommand, bool>
{

    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IMapper _mapper;
    public CreateClassInformationCommandHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
    {
        _classInformationRepository = classInformationRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateUpdateClassInformationCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);
            //Check if the class existed
            if (classInformation is not null)
            {
                classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);

                classInformation.LastModificationTime = DateTime.Now;
                //classInformation.Description = command.ClassInformationDto.Description;

                _classInformationRepository.Update(classInformation);

                return true;
            }

            classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);

            await _classInformationRepository.Insert(classInformation);


            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when class is adding or updating." + ex.Message);
        }

    }


}

