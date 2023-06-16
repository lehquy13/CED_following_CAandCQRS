using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class
    GetTutorProfileQueryHandler : GetByIdQueryHandler<GetTutorProfileQuery, TutorProfileDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITutorRepository _userRepository;

    public GetTutorProfileQueryHandler(
        IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        ISubjectRepository subjectRepository,
        ITutorRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _requestGettingClassRepository = requestGettingClassRepository;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _userRepository = userRepository;
    }

    public override async Task<TutorProfileDto?> Handle(
        GetTutorProfileQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var tutor = await _userRepository.GetById(query.Guid);
            if (tutor is null)
            {
                
                throw new Exception("The tutor does not exist!");
            }
            var requests = _requestGettingClassRepository.GetAll()
                .Where(x => x.TutorId.Equals(tutor.Id))
                .ToList();
            var classes = await _classInformationRepository.GetAllList();
            var subjects = await _subjectRepository.GetAllList();
            var classInformations = 
                requests.GroupJoin(
                    classes,
                    d=>d.ClassInformationId,
                    c => c.Id,
                    (d,c)=>
                    {
                        var classIn = c.FirstOrDefault();
                        var subjectName = subjects.FirstOrDefault(x => x.Id.Equals(classIn?.SubjectId))?.Name;
                        return (d, classIn,subjectName).Adapt<RequestGettingClassDto>();;
                    });
            
            var classInformationsL = classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize).ToList();

            var resultPaginatedList = PaginatedList<RequestGettingClassDto>.CreateAsync(classInformationsL,
                query.PageIndex, query.PageSize, classInformationsL.Count);
            //get tutor majors
            
            var subjectDtos = _tutorMajorRepository.GetAll().Where(x => x.TutorId.Equals(tutor.Id)).ToList()
                .GroupJoin(
                subjects,
                major => major.SubjectId,
                s => s.Id,
                (major, s) => _mapper.Map<SubjectDto>(s.First())
            ).ToList();
            //get tutor verifications

            var verifications = _tutorVerificationInfoRepository.GetAll().Where(x => x.TutorId.Equals(tutor.Id))
                .ToList();
          

            return (tutor,resultPaginatedList,subjectDtos,verifications).Adapt<TutorProfileDto>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}