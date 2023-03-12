using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class CreateSubjectCommandHandler
    : IRequestHandler<CreateUpdateSubjectCommand, bool>
{
   
    private readonly ISubjectRepository _subjectRepository;
    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {      
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(CreateUpdateSubjectCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var subject = await _subjectRepository.GetSubjectByName(command.SubjectDto.Name);
            //Check if the subject existed
            if (subject is not null)
            {
                subject.LastModificationTime = DateTime.Now;
                subject.Description = command.SubjectDto.Description;
                
                _subjectRepository.Update(subject);

                return true;
            }

            subject = new Subject
            {
                Name = command.SubjectDto.Name,
                Description = command.SubjectDto.Description
            };

            await _subjectRepository.Insert(subject);


            return true;
        }
        catch(Exception ex)
        {
            throw new Exception("Error happens when subject is adding or updating." + ex.Message);
        }
        
    }

    
}

