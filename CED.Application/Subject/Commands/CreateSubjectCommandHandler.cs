using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class CreateSubjectCommandHandler
    : IRequestHandler<CreateSubjectCommand, bool>
{
   
    private readonly ISubjectRepository _subjectRepository;
    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {      
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(CreateSubjectCommand command, CancellationToken cancellationToken)
    {

        //Check if the subject existed
        if (await _subjectRepository.GetSubjectByName(command.SubjectDto.Name) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("Subject has already existed");
            //return false;
        }

        var subject = new Subject
        {
            Name = command.SubjectDto.Name,
            Description= command.SubjectDto.Description
        };

        await _subjectRepository.Insert(subject);


        return true;
    }

    
}

