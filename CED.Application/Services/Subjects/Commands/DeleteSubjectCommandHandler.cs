using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Commands;

public class DeleteSubjectCommandHandler
    : DeleteCommandHandler<DeleteSubjectCommand>
{

    private readonly ISubjectRepository _subjectRepository;
    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository) : base()
    {
        _subjectRepository = subjectRepository;
    }
    public override async Task<bool> Handle(DeleteSubjectCommand command, CancellationToken cancellationToken)
    {
        //Check if the subject existed
        Subject? subject = await _subjectRepository.GetById(command.id);
        if (subject is null)
        {
            throw new Exception("Subject has already existed");
        }

        _subjectRepository.Delete(subject);

        return true;
    }
}

