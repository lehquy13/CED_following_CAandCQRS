using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

//Not using currently
public class GetStudentByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<StudentDto>, StudentDto> 
{
    private readonly IUserRepository _userRepository;
    public GetStudentByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<StudentDto?> Handle(GetObjectQuery<StudentDto> query, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null) { return null; }
            StudentDto result = _mapper.Map<StudentDto>(user);
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

