using CED.Application.Common.Persistence;
using CED.Contracts.Subjects;
using CED.Domain.Entities.Subjects;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Subjects.Queries;

public class GetSubjectQueryHandler
    : IRequestHandler<GetSubjectQuery, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<SubjectDto> Handle(GetSubjectQuery query, CancellationToken cancellationToken)
    {

        Subject? subject = await _subjectRepository.GetById(query.id);
        if (subject == null)
        {
            throw new Exception("the subject doesn't exist.");

        }
        return _mapper.Map<SubjectDto>(subject);


    }
}

