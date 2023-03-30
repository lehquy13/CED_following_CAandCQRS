using CED.Application.Common.Services.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQueryHandler : GetByIdQueryHandler<GetSubjectQuery, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<SubjectDto?> Handle(GetSubjectQuery query, CancellationToken cancellationToken)
    {
        Subject? subject = await _subjectRepository.GetById(query.id);
        if (subject == null)
        {
            return null;
            throw new Exception("the subject doesn't exist.");

        }
        return _mapper.Map<SubjectDto>(subject);
    }
}

