using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }

    // public async Task Insert(User entity)
    // {
    //     try
    //     {
    //         await Context.Set<User>().AddAsync(entity);
    //
    //         await Context.SaveChangesAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception(ex.Message);
    //     }
    // }

    public List<User> GetTutors()
    {
        try
        {
            var users = Context.Set<User>().AsEnumerable().Where(o => o is { Role: UserRole.Tutor, IsDeleted: false })
                .ToList();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<User> GetStudents()
    {
        try
        {
            var users = Context.Set<User>().AsEnumerable().Where(o => o.Role == UserRole.Learner).ToList();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ExistenceCheck(string? email)
    {
        try
        {
            if (email != null)
            {
                return await Context.Set<User>().AnyAsync(o => o.Email.Equals(email));
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var user = await Context.Set<User>().FirstOrDefaultAsync(o => o.Email == email);

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}