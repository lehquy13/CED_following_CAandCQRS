using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetRequestGettingClassDetailQueryHandler 
    : GetByIdQueryHandler<GetObjectQuery<Result<RequestGettingClassMinimalDto>>,
        Result<RequestGettingClassMinimalDto>?>

{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetRequestGettingClassDetailQueryHandler(
        IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<RequestGettingClassMinimalDto>?> Handle(
        GetObjectQuery<Result<RequestGettingClassMinimalDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var requests = await _requestGettingClassRepositoryepository.GetById(query.Guid);
            if (requests is null)
            {
                throw new Exception("This getting class request does not exist!");
            }
           
            var classes = await _classInformationRepository.GetById(requests.ClassInformationId);
            var user = await _userRepository.GetById(requests.TutorId);

            return (requests, user).Adapt<RequestGettingClassMinimalDto>();
            
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    
}