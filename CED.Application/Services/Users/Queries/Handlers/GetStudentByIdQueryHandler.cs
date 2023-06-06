using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

//Not using currently
public class GetStudentByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<LearnerDto>, LearnerDto> 
{
    private readonly IUserRepository _userRepository;
    public GetStudentByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<LearnerDto?> Handle(GetObjectQuery<LearnerDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null) { return null; }
            LearnerDto result = _mapper.Map<LearnerDto>(user);
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

