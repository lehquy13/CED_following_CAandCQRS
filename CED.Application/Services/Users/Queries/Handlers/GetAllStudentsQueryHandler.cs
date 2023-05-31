using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetAllStudentsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<StudentDto>>, StudentDto>
{
    private readonly IUserRepository _userRepository;

    public GetAllStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<StudentDto>> Handle(GetObjectQuery<PaginatedList<StudentDto>> query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetStudents();
            var result = _mapper.Map<List<StudentDto>>(users.Where(x => x.IsDeleted is false)
                .Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                .ToList());
            
            return PaginatedList<StudentDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);
             
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}