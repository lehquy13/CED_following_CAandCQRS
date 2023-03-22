using CED.Application.Common.Services.QueryHandlers;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.UsersInformations.Queries;

public class GetUserByIdQueryHandler<TDto> : GetByIdQueryHandler<GetUserByIdQuery<TDto>, TDto?> where TDto : class
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<TDto?> Handle(GetUserByIdQuery<TDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Id);
            if(user is null) { return null; }
            TDto? result = _mapper.Map<TDto>(user);
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

