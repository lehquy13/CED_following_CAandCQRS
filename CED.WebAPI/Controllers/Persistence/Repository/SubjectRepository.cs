using CED.Domain.Subjects;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }

    public async Task<Subject?> GetSubjectByName(string name)
    {
        try
        {
            return await _context.Set<Subject>().FirstOrDefaultAsync(o => o.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

