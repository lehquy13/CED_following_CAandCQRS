using CED.Application.Common.Persistence;
using CED.Contracts.Entities.Subject;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.Login;

public class GetSubjectsQueryHandler
    : IRequestHandler<GetSubjectsQuery, List<SubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper= mapper;
    }
    public async Task<List<SubjectDto>> Handle(GetSubjectsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var subjects = await _subjectRepository.GetAllList();

            return _mapper.Map<List<SubjectDto>>(subjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }
}

