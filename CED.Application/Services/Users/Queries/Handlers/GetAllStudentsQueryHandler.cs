using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetAllStudentsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<StudentDto>>, StudentDto>
{
    private readonly IUserRepository _userRepository;
    public GetAllStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<List<StudentDto>> Handle(GetObjectQuery<List<StudentDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetStudents();
            var reuslt = _mapper.Map<List<StudentDto>>(users.Where(x => x.IsDeleted is false)
                                                            .ToList());
            return reuslt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

