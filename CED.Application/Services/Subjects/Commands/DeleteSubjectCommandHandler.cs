using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using LazyCache;
using Newtonsoft.Json;

namespace CED.Application.Services.Subjects.Commands;

public class DeleteSubjectCommandHandler
    : DeleteCommandHandler<DeleteSubjectCommand>
{

    private readonly ISubjectRepository _subjectRepository;
    private readonly IAppCache _cache;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IAppCache appCache) : base()
    {
        _subjectRepository = subjectRepository;
        _cache = appCache;
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
        var defaultRequest = new GetObjectQuery<PaginatedList<SubjectDto>>();
        _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));

        return true;
    }
}

