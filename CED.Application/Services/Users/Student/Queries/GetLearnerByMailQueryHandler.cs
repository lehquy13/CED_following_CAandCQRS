using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Users.Student.Queries;

public class GetLearnerByMailQueryHandler : GetByIdQueryHandler<GetLearnerByMailQuery, LearnerDto>
{

    private readonly IUserRepository _userRepository;
    private readonly IClassInformationRepository _classInformationRepository;

    public GetLearnerByMailQueryHandler(IClassInformationRepository classInformationRepository,
        IMapper mapper, IUserRepository userRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<LearnerDto>> Handle(GetLearnerByMailQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetUserByEmail(query.Email);
           
            if (user is null)
            {
                return Result.Fail(new NonExistUserError());
            }

            var classInfors = await _classInformationRepository.GetLearningClassInformationsByUserId(user.Id);
            
            var learner = _mapper.Map<LearnerDto>(user);
            learner.LearningClassInformations = PaginatedList<ClassInformationForListDto>.CreateAsync(
                _mapper.Map<List<ClassInformationForListDto>>(classInfors),
                1,
                100
            );
            
            return learner;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}