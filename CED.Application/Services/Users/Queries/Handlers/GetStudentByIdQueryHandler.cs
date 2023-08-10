using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using FluentResults;
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
    public override async Task<Result<LearnerDto>> Handle(GetObjectQuery<LearnerDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.ObjectId);
            if (user is null) { return Result.Fail(new NonExistUserError()); }
            LearnerDto result = _mapper.Map<LearnerDto>(user);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

