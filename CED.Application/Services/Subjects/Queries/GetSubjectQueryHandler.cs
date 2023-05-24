using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQueryHandler : GetByIdQueryHandler<GetObjectQuery<SubjectDto>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<SubjectDto?> Handle(GetObjectQuery<SubjectDto> query, CancellationToken cancellationToken)
    {
        Subject? subject = await _subjectRepository.GetById(query.Guid);
        if (subject == null)
        {
            return null;
        }
        return _mapper.Map<SubjectDto>(subject);
    }
}

