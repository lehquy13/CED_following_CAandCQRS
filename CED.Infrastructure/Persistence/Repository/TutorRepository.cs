using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class TutorRepository : Repository<Tutor>, ITutorRepository
{
    public TutorRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }


   

    public async Task<Tutor?> GetUserByEmail(string email)
    {
        try
        {
            var user = await Context.Set<User>().FirstOrDefaultAsync(o => o.Email == email);
            
            if (user == null) { return null; }
            var tutor = await Context.Set<Tutor>().FirstOrDefaultAsync(o => o.Id.Equals(user.Id));

            return tutor;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<TutorFull>> GetAllsWithFullInformation()
    {
        try
        {
            var user = await Context.Set<User>()
                .Where(x => x.IsDeleted == false)
                .Join(
                    Context.Set<Tutor>(),
                    u => u.Id,
                    tu => tu.Id,
                    (u,tu) => 
                    new TutorFull(u,tu)
                ).ToListAsync();
            return user;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }
}

