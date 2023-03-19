using CED.Application.Common.Services.CommandHandlers;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateClassInformationCommandHandler 
    : CreateCommandHandler<CreateUpdateClassInformationCommand>
{
    private readonly IClassInformationRepository _classInformationRepository;

    public CreateClassInformationCommandHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
        :base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<bool> Handle(CreateUpdateClassInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);
            //Check if the class existed
            if (classInformation is not null)
            {
                classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);
                classInformation.LastModificationTime = DateTime.Now;

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

