using CED.Application.Common.Services.QueryHandlers;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.UsersInformations.Queries;

public class GetUsersQueryHandler<TDto> : GetAllQueryHandler<GetUsersQuery<TDto>, TDto> where TDto : class
{
    private readonly IUserRepository _userRepository;
    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<TDto>> Handle(GetUsersQuery<TDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            if (query.UserRole == Domain.Shared.ClassInformationConsts.UserRole.All)
                return _mapper.Map<List<TDto>>(await _userRepository.GetAllList());

            var users = from user in _userRepository.GetAll()
                        where user.Role == query.UserRole
                        select user;
            List<User> list = users.ToList();
            var reuslt = _mapper.Map<List<TDto>>(list);
            return reuslt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

