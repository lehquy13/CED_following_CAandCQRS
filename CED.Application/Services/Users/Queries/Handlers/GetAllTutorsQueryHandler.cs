using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users.Tutors;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;
/// <summary>
/// Deprecated! Use GetAllTutorInformationsAdvancedQuery instead!
/// </summary>
public class GetAllTutorsQueryHandler //: GetAllQueryHandler<GetObjectQuery<List<TutorForDetailDto>>, TutorForDetailDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ISubjectRepository _subjectRepository;


    public GetAllTutorsQueryHandler(IUserRepository userRepository,        ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) //: base(mapper)
    {
        _subjectRepository = subjectRepository;

        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;

    }

    public  async Task<List<TutorForDetailDto>> Handle(GetObjectQuery<List<TutorForDetailDto>> query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new InvalidOperationException("No more using!!!");
    }
}