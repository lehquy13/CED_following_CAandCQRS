using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }

    public List<User> GetTutors()
    {
        try
        {
            var users  =  _context.Set<User>().AsEnumerable().Where(o => o.Role == UserRole.Tutor).ToList();
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
            var users  =  _context.Set<User>().AsEnumerable().Where(o => o.Role == UserRole.Student).ToList();
            return users;
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
            var user = await _context.Set<User>().FirstOrDefaultAsync(o => o.Email == email);
            if (user == null) { return null; }
            return user;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }

    public List<User> GetUsersByRole(UserRole userRole = UserRole.All)
    {
        try
        {
            if(userRole == UserRole.All)
            {
                return _context.Set<User>().AsEnumerable().ToList();
            }
           
            return _context.Set<User>().AsEnumerable().Where(o => o.Role == UserRole.Student).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

