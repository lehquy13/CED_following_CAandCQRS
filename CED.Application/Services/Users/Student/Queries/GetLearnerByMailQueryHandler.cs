using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Student.Queries;

public class GetLearnerByMailQueryHandler : GetByIdQueryHandler<GetLearnerByMailQuery, LearnerDto?>
{

    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetLearnerByMailQueryHandler(IUserRepository userRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {

        _tutorMajorRepository = tutorMajorRepository;
        _userRepository = userRepository;
    }

    public override async Task<LearnerDto?> Handle(GetLearnerByMailQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetUserByEmail(query.Email);
            if (user is null)
            {
                return null;
            }
            var learner = _mapper.Map<LearnerDto>(user);
           
            return learner;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}