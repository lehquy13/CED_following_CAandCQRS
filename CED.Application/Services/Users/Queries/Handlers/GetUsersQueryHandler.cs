using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetUsersQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<UserDto>>, UserDto>
{
    private readonly IUserRepository _userRepository;
    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<PaginatedList<UserDto>> Handle(GetObjectQuery<PaginatedList<UserDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = await _userRepository.GetAllList();

            var result = _mapper.Map<List<UserDto>>(users.Where(x => x.IsDeleted is false)
                .Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                .ToList());
            
            return PaginatedList<UserDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

