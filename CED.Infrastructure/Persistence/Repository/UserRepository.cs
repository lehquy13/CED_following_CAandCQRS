using CED.Application.Common.Persistence;
using CED.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(o => o.Email == email);
            if (user == null) { return null; }
            return user;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }
}

