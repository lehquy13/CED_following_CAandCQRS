using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;

namespace CED.Application.Services.Users.Student.Queries;

public class GetLearnerByMailQuery : GetObjectQuery<LearnerDto>
{
    public string Email = string.Empty;
}