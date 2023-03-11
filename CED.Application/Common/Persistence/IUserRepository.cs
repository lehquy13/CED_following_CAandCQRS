using CED.Domain.Entities.User;
namespace CED.Application.Common.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}

