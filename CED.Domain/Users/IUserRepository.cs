namespace CED.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}

