using CED.Domain.Entities;

namespace CED.Application.Common.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);

}

