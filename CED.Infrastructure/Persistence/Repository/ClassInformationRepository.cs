using CED.Domain.ClassInformations;

namespace CED.Infrastructure.Persistence.Repository;


public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {

    }

    public List<ClassInformation> GetLearningClassInformationsByUserId(Guid guid)
    {
        var result = _context.Set<ClassInformation>()
                             .AsEnumerable()
                             .Where(x => x.StudentId.Equals(guid))
                             .ToList();
        return result;
    }

    public List<ClassInformation> GetTeachingClassInformationsByUserId(Guid guid)
    {
        var result = _context.Set<ClassInformation>()
                             .AsEnumerable()
                             .Where(x => x.TutorId.Equals(guid))
                             .ToList();
        return result;
    }
}

