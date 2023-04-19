using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetStudentsQueryHandler : GetAllQueryHandler<GetUsersQuery<StudentDto>, StudentDto>
{
    private readonly IUserRepository _userRepository;
    public GetStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<StudentDto>> Handle(GetUsersQuery<StudentDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetStudents();
            var reuslt = _mapper.Map<List<StudentDto>>(users);
            return reuslt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

