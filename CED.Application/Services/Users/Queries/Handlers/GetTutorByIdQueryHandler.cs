using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetTutorByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorDto>, TutorDto> 
{
    private readonly IUserRepository _userRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    public GetTutorByIdQueryHandler(IUserRepository userRepository,ITutorRepository tutorRepository,ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<TutorDto?> Handle(GetObjectQuery<TutorDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            Domain.Users.Tutor? tutor = await _tutorRepository.GetById(query.Guid);
            if (user is null || tutor is null) { return null; }
            TutorDto result = (user,tutor).Adapt<TutorDto>();

            var tutorMajors = _tutorMajorRepository.GetAll().Where(x => x.TutorId.Equals(user.Id)).ToList();
            
            foreach (var i in tutorMajors.Select(x => x.SubjectId))
            {
                var s = await _subjectRepository.GetById(i);
                if (s is not null)
                {
                    result.Majors.Add(_mapper.Map<SubjectDto>(s));
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

