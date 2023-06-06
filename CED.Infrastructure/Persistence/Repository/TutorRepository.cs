using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class TutorRepository : Repository<Tutor>, ITutorRepository
{
    public TutorRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }

    public List<Tutor> GetTutors()
    {
        try
        {
            var users  =  Context.Set<Tutor>().AsEnumerable().Where(o => o is { Role: UserRole.Tutor, IsDeleted: false }).ToList();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
   

    public async Task<Tutor?> GetUserByEmail(string email)
    {
        try
        {
            var user = await Context.Set<Tutor>().FirstOrDefaultAsync(o => o.Email == email);
            if (user == null) { return null; }
            return user;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }

    
}

