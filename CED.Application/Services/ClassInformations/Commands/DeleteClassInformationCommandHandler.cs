using CED.Application.Common.Services.CommandHandlers;
using CED.Domain.ClassInformations;

namespace CED.Application.Services.ClassInformations.Commands;

public class DeleteClassInformationCommandHandler
    : DeleteCommandHandler<DeleteClassInformationCommand>
{

    private readonly IClassInformationRepository _classInformationRepository;
    public DeleteClassInformationCommandHandler(IClassInformationRepository classInformationRepository):base()
    {
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<bool> Handle(DeleteClassInformationCommand command, CancellationToken cancellationToken)
    {

        //Check if the class existed
        ClassInformation? classInformation = await _classInformationRepository.GetById(command.id);
        if (classInformation is null)
        {
            throw new Exception("Class has already existed");
        }

        _classInformationRepository.Delete(classInformation);

        return true;
    }
}

