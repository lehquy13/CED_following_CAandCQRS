using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetAllStudentsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<LearnerDto>>, LearnerDto>
{
    private readonly IUserRepository _userRepository;

    public GetAllStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<LearnerDto>> Handle(GetObjectQuery<PaginatedList<LearnerDto>> query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = _userRepository.GetStudents();
            var result = _mapper.Map<List<LearnerDto>>(users.Where(x => x.IsDeleted is false)
                .Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                .ToList());
            
            return PaginatedList<LearnerDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);
             
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}