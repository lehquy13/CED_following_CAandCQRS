using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Tutor.Queries;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class
    GetAllRequestGettingClassQueryHandler : GetAllQueryHandler<GetAllRequestGettingClassQuery,
        RequestGettingClassDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITutorRepository _userRepository;

    public GetAllRequestGettingClassQueryHandler(
        IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        ISubjectRepository subjectRepository,
        ITutorRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<RequestGettingClassDto>> Handle(
        GetAllRequestGettingClassQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var classInformation = await _classInformationRepository.GetById(query.Guid);
            if (classInformation is null)
            {
                throw new Exception("The class doesn't exist");
            }
            var requests = _requestGettingClassRepositoryepository.GetAll()
                .Where(x => x.ClassInformationId.Equals(classInformation.Id))
                .ToList();
            var subject = await _subjectRepository.GetById(classInformation.SubjectId);

            var requestGettingClassDtos = requests.GroupJoin(
                _userRepository.GetAll(),
                re => re.TutorId,
                user => user.Id,
                (req, user) => (req, user.FirstOrDefault(), subject?.Name ?? "null subject").Adapt<RequestGettingClassDto>()

            ).ToList();

            var resultPaginatedList = PaginatedList<RequestGettingClassDto>.CreateAsync(requestGettingClassDtos,
                query.PageIndex, query.PageSize, requestGettingClassDtos.Count);

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}