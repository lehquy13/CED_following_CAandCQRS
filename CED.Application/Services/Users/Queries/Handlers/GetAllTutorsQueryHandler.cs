using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetAllTutorsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<TutorDto>>, TutorDto>
{
    private readonly IUserRepository _userRepository;

    public GetAllTutorsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<List<TutorDto>> Handle(GetObjectQuery<List<TutorDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetTutors();
            var result = _mapper.Map<List<TutorDto>>( 
                users.
            Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList()
            );
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}