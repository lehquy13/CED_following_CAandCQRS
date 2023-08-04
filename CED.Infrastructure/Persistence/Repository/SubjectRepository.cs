using CED.Domain.Subjects;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(AppDbContext cEdDbContext) : base(cEdDbContext)
    {
    }

    public async Task<Subject?> GetSubjectByName(string name)
    {
        try
        {
            return await _appDbContext.Set<Subject>().FirstOrDefaultAsync(o => o.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Subject>> GetTutorMajors(Guid tutorId)
    {
        try
        {
            return await _appDbContext.Set<TutorMajor>()
                .Where(x => x.TutorId == tutorId)
                .Include(x => x.Subject)
                .Select(x => x.Subject)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

