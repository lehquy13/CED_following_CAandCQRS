using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

//Not using currently
public class GetTutorByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorDto>, TutorDto> 
{
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    public GetTutorByIdQueryHandler(IUserRepository userRepository,ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<TutorDto?> Handle(GetObjectQuery<TutorDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null) { return null; }
            TutorDto result = _mapper.Map<TutorDto>(user);

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

