using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUsersQueryHandler : GetAllQueryHandler<GetObjectQuery<List<UserDto>>, UserDto>
{
    private readonly IUserRepository _userRepository;
    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<UserDto>> Handle(GetObjectQuery<List<UserDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = await _userRepository.GetAllList();

            var result = _mapper.Map<List<UserDto>>(
                users
                .Where(x => x.IsDeleted is false)
                .ToList()
                );
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

