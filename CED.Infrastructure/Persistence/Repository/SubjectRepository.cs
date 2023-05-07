using CED.Domain.Subjects;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(CEDDBContext cEdDbContext) : base(cEdDbContext)
    {
    }

    public async Task<Subject?> GetSubjectByName(string name)
    {
        try
        {
            return await Context.Set<Subject>().FirstOrDefaultAsync(o => o.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

