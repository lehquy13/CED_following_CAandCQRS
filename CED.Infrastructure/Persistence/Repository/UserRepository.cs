using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext cEDDBContext) : base(cEDDBContext)
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

    public async Task<List<User>> GetTutors()
    {
        try
        {
            var users = await _appDbContext.Set<User>().Where(x => x.IsDeleted == false && x.Role == UserRole.Tutor).ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task< List<User>> GetStudents()
    {
        try
        {
            var users = await _appDbContext.Set<User>().Where(o => o.Role == UserRole.Learner && o.IsDeleted == false).ToListAsync();
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
                return await _appDbContext.Set<User>().AnyAsync(o => o.Email.Equals(email));
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(o => o.Email == email);

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}