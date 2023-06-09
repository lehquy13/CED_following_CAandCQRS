using System.Linq.Dynamic.Core;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries;

public class GetTeachingClassDetailQueryHandler 
    : GetByIdQueryHandler<GetObjectQuery<Result<RequestGettingClassDto>>,
        Result<RequestGettingClassDto>?>

{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetTeachingClassDetailQueryHandler(
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

    public override async Task<Result<RequestGettingClassDto>?> Handle(
        GetObjectQuery<Result<RequestGettingClassDto>> query, CancellationToken cancellationToken)
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
            var subjects = await _subjectRepository.GetById(classes?.SubjectId ?? Guid.Empty);

            return (requests, classes, subjects?.Name ?? "null subject").Adapt<RequestGettingClassDto>();
            
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    
}