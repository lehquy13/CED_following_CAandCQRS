using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.CustomerQueries;

public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserForDetailDto>, UserForDetailDto>
{

    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<Result<UserForDetailDto>> Handle(GetObjectQuery<UserForDetailDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.ObjectId);
            if (user is null) { return Result.Fail(new NonExistUserError()); }
            UserForDetailDto result = _mapper.Map<UserForDetailDto>(user);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

