using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using FluentResults;
using LazyCache;
using MediatR;
using Newtonsoft.Json;

namespace CED.Application.Services.Subjects.Commands;

public class DeleteSubjectCommandHandler
    : DeleteCommandHandler<DeleteSubjectCommand>
{

    private readonly ISubjectRepository _subjectRepository;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IAppCache cache, IUnitOfWork unitOfWork,
        IPublisher publisher) : base(unitOfWork, cache, publisher)
    {
        _subjectRepository = subjectRepository;
    }
    public override async Task<Result<bool>> Handle(DeleteSubjectCommand command, CancellationToken cancellationToken)
    {
        //Check if the subject existed
        Subject? subject = await _subjectRepository.GetById(command.SubjectId);
        if (subject is null)
        {
            return Result.Fail("Subject does not exist");
        }

        _subjectRepository.Delete(subject);
        var defaultRequest = new GetObjectQuery<PaginatedList<SubjectDto>>();
        
        if(await _unitOfWork.SaveChangesAsync() <= 0)
        {
            return Result.Fail("Delete failed");
        }
        
        _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
        return true;
    }
}

