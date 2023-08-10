using CED.Domain.Repository;

namespace CED.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    Task<List<User>> GetStudents();
    Task<List<User>> GetTutors();
}