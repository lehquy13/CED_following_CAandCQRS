using CED.Application.Common.Errors.ClassInformations;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQueryHandler : GetByIdQueryHandler<GetObjectQuery<SubjectDto>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<Result<SubjectDto>> Handle(GetObjectQuery<SubjectDto> query, CancellationToken cancellationToken)
    {
        Subject? subject = await _subjectRepository.GetById(query.ObjectId);
        if (subject == null)
        {
            return Result.Fail(new NonExistSubjectError());
        }
        return _mapper.Map<SubjectDto>(subject);
    }
}

