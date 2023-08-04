using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries.GetLearningClassesOfUserQuery;

/// <summary>
/// note: this query currently is not used
/// </summary>
public class GetLearningClassesOfUserQueryHandler : GetAllQueryHandler<GetLearningClassesOfUserQuery, ClassInformationForListDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetLearningClassesOfUserQueryHandler(
        IClassInformationRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<PaginatedList<ClassInformationForListDto>>> Handle(GetLearningClassesOfUserQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var student = await _userRepository.GetById(query.ObjectId);
            if (student is null)
            {
                throw new Exception("The user does not exist!");
            }

            var classInformations = await _classInformationRepository
                .GetLearningClassInformationsByUserId(query.ObjectId);
                
            var subjects =  await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors();
            
            var classInformationDtos =
                _mapper.Map<List<ClassInformationForListDto>>(classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize));
            
            var resultPaginatedList = PaginatedList<ClassInformationForListDto>.CreateAsync(classInformationDtos,
                query.PageIndex, query.PageSize, classInformationDtos.Count);


            

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

