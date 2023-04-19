using CED.Application.Common.Services.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUsersQueryHandler : GetAllQueryHandler<GetUsersQuery<UserDto>, UserDto>
{
    private readonly IUserRepository _userRepository;
    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<UserDto>> Handle(GetUsersQuery<UserDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = await _userRepository.GetAllList();

            var reuslt = _mapper.Map<List<UserDto>>(users);
            return reuslt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

