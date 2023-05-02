using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUpdateSubjectCommandHandler : CreateUpdateCommandHandler<CreateUpdateSubjectCommand>
{

    private readonly ISubjectRepository _subjectRepository;

    public CreateUpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<bool> Handle(CreateUpdateSubjectCommand command, CancellationToken cancellationToken)
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

            subject = _mapper.Map<Subject>(command.SubjectDto);

            await _subjectRepository.Insert(subject);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when subject is adding or updating." + ex.Message);
        }
    }
}

