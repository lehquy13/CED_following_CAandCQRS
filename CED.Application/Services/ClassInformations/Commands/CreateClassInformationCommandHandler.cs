using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.ClassInformations;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateClassInformationCommandHandler 
    : CreateUpdateCommandHandler<CreateUpdateClassInformationCommand>
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

