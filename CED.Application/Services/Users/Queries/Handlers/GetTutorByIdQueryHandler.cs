using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

//Not using currently
public class GetTutorByIdQueryHandler : GetByIdQueryHandler<GetUserByIdQuery<TutorDto>, TutorDto> 
{
    private readonly IUserRepository _userRepository;
    public GetTutorByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<TutorDto?> Handle(GetUserByIdQuery<TutorDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Id);
            if (user is null) { return null; }
            TutorDto result = _mapper.Map<TutorDto>(user);
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

