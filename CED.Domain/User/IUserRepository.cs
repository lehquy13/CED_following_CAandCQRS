namespace CED.Domain.User;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}

