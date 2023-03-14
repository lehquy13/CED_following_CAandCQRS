using CED.Application.Common.Persistence;
using CED.Domain.Entities.ClassInformations;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

public class DeleteClassInformationCommandHandler
    : IRequestHandler<DeleteClassInformationCommand, bool>
{

    private readonly IClassInformationRepository _classInformationRepository;
    public DeleteClassInformationCommandHandler(IClassInformationRepository classInformationRepository)
    {
        _classInformationRepository = classInformationRepository;
    }
    public async Task<bool> Handle(DeleteClassInformationCommand command, CancellationToken cancellationToken)
    {

        //Check if the class existed
        ClassInformation? classInformation = await _classInformationRepository.GetById(command.id);
        if (classInformation is null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("Class has already existed");
            //return false;
        }

        _classInformationRepository.Delete(classInformation);


        return true;
    }


}

