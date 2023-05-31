using CED.Domain.ClassInformations;
using CED.Infrastructure.Entity_Framework_Core;

namespace CED.Infrastructure.Persistence.Repository;


public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {

    }

    public IEnumerable<ClassInformation> GetLearningClassInformationsByUserId(Guid guid)
    {
        var result = Context.Set<ClassInformation>()
            .AsEnumerable()
            .Where(x => x.StudentId.Equals(guid));
        return result;
    }

    public IEnumerable<ClassInformation> GetTeachingClassInformationsByUserId(Guid guid)
    {
        var result = Context.Set<ClassInformation>()
            .AsEnumerable()
            .Where(x => x.TutorId.Equals(guid));
        return result;
    }
}

