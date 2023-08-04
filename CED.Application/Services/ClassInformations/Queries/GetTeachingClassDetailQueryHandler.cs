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

// Note: check this query handler later bc of it may violate DDD
public class GetRequestGettingClassDetailQueryHandler : GetByIdQueryHandler<GetObjectQuery<RequestGettingClassMinimalDto>, RequestGettingClassMinimalDto>

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

    public override async Task<Result<RequestGettingClassMinimalDto>> Handle(GetObjectQuery<RequestGettingClassMinimalDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var requests = await _requestGettingClassRepositoryepository.GetById(query.ObjectId);
            if (requests is null)
            {
                return Result.Fail("This getting class request does not exist!");
            }
           
            var classes = await _classInformationRepository.GetById(requests.ClassInformationId);
            if (classes is null)
            {
                return Result.Fail("This class does not exist!");
            }

            var result = _mapper.Map<RequestGettingClassMinimalDto>(requests);
            return result;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    
}