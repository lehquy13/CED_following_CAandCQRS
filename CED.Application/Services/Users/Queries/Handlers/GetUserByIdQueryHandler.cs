using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserDto>, UserDto>
{

    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<Result<UserDto>> Handle(GetObjectQuery<UserDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.ObjectId);
            if (user is null) { return Result.Fail(new NonExistUserError()); }
            UserDto result = _mapper.Map<UserDto>(user);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

