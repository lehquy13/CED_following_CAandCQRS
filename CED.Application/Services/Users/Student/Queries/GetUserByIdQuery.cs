using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Student.Queries;

public class GetLearnerByMailQuery : IRequest<LearnerDto?> 
{
    public string Email = string.Empty;
}