using CED.Domain.ClassInformations;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class RequestGettingClassRepository : Repository<RequestGettingClass>, IRequestGettingClassRepository
{
    public RequestGettingClassRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<RequestGettingClass>> GetAllRequestGettingClass(Guid classId)
    {
        return await _appDbContext.RequestGettingClasses.Where(x => x.ClassInformationId == classId).ToListAsync();
    }

    public async Task<bool> IsRequested(Guid tutorId, Guid classId)
    {
        return await _appDbContext.RequestGettingClasses.Where(x => x.ClassInformationId == classId && x.TutorId == tutorId).AnyAsync();

    }
}