using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    List<User> GetStudents();
    Task<bool> ExistenceCheck(string? email);
    List<User> GetTutors();
}