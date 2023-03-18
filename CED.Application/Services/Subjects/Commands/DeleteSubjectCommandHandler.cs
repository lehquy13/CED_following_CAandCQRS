using CED.Domain.Subjects;
using MediatR;

namespace CED.Application.Services.Subjects.Commands;

public class DeleteSubjectCommandHandler
    : IRequestHandler<DeleteSubjectCommand, bool>
{

    private readonly ISubjectRepository _subjectRepository;
    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(DeleteSubjectCommand command, CancellationToken cancellationToken)
    {

        //Check if the subject existed
        Subject? subject = await _subjectRepository.GetById(command.id);
        if (subject is null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("Subject has already existed");
            //return false;
        }

        _subjectRepository.Delete(subject);


        return true;
    }


}

