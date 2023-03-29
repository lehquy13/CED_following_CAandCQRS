using CED.Application.Common.Services.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.UsersInformations.Queries.Handlers;

public class GetTutorsQueryHandler : GetAllQueryHandler<GetUsersQuery<TutorDto>, TutorDto>
{
    private readonly IUserRepository _userRepository;
    public GetTutorsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<TutorDto>> Handle(GetUsersQuery<TutorDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetTutors();
            var reuslt = _mapper.Map<List<TutorDto>>(users);
            return reuslt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

