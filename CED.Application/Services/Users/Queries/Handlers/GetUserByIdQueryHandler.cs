using Castle.MicroKernel.ModelBuilder.Descriptors;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserDto>, UserDto?>
{
    private readonly ISubjectRepository _subjectRepository;

    private readonly ITutorRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetUserByIdQueryHandler(ITutorRepository userRepository,ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;

        _tutorMajorRepository = tutorMajorRepository;
        _userRepository = userRepository;
    }
    public override async Task<UserDto?> Handle(GetObjectQuery<UserDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null) { return null; }
            UserDto? result = _mapper.Map<UserDto>(user);

            // if (result.Role == UserRole.Tutor)
            // {
            //     var tutorsMajors = _tutorMajorRepository.GetAll()
            //         .Where(t => t.TutorId == result.Id)
            //         .Select(major => major.SubjectId);
            //     var subjects = await _subjectRepository.GetAllList();
            //
            //     foreach (var tm in tutorsMajors)
            //     {
            //         var subject = subjects.FirstOrDefault(s => s.Id == tm);
            //         if (subject is not null)
            //         {
            //             result.SubjectDtos.Add(_mapper.Map<SubjectDto>(subject));
            //         }
            //     }
            //     
            //
            // }
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

