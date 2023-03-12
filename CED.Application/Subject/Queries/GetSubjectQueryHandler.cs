using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.Login;

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

