using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserDto>, UserDto?>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<UserDto?> Handle(GetObjectQuery<UserDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null) { return null; }
            UserDto? result = _mapper.Map<UserDto>(user);
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

