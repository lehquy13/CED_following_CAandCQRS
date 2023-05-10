using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Commands;

public class UpdateTutorForClassCommandHandler 
    : CreateUpdateCommandHandler<UpdateTutorForClassCommand>
{
    private readonly IClassInformationRepository _classInformationRepository;

    public UpdateTutorForClassCommandHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
        :base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<bool> Handle(UpdateTutorForClassCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);
            
            //Check if the class existed
            if (classInformation is not null)
            {
                command.ClassInformationDto.TutorDtoId = command.TutorId;
                var classInformation1 = _mapper.Map<ClassInformation>(command.ClassInformationDto);
                classInformation1.LastModificationTime = DateTime.Now;

                _classInformationRepository.Update(classInformation1);

                return true;
            }

            classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);
            //classInformation = _mapper.From(command.ClassInformationDto).Adapt<ClassInformation>();
            await _classInformationRepository.Insert(classInformation);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when class is adding or updating." + ex.Message);
        }
    }
}

