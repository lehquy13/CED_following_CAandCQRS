using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUpdateSubjectCommandHandler : CreateUpdateCommandHandler<CreateUpdateSubjectCommand>
{
    private readonly ISubjectRepository _subjectRepository;
    public CreateUpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IAppCache cache,
        ILogger<CreateUpdateSubjectCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork, IPublisher publisher)
        : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<Result<bool>> Handle(CreateUpdateSubjectCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var subject = await _subjectRepository.GetSubjectByName(command.SubjectDto.Name);
            var task = "creating";
            //Check if the subject existed
            if (subject is not null)
            {
                subject.LastModificationTime = DateTime.Now;
                subject.Description = command.SubjectDto.Description;
                task = "updating";
            }
            else
            {
                subject = _mapper.Map<Subject>(command.SubjectDto);
                await _subjectRepository.Insert(subject);
            }

            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail($"Fail to save changes to database when {task} subject.");
            }

            var defaultRequest = new GetObjectQuery<PaginatedList<SubjectDto>>();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when subject is adding or updating." + ex.Message);
        }
    }
}