using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class CreateSubjectCommandHandler
    : IRequestHandler<CreateUpdateSubjectDto, bool>
{
   
    private readonly ISubjectRepository _subjectRepository;
    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {      
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(CreateUpdateSubjectDto command, CancellationToken cancellationToken)
    {

        //Check if the subject existed
        if (await _subjectRepository.GetSubjectByName(command.Name) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("Subject has already existed");
        }

        var subject = new Subject
        {
            Name = command.Name,
            Description= command.Description
        };

        await _subjectRepository.Insert(subject);


        return true;
    }

    
}

