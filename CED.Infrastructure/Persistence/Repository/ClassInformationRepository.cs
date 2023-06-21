using CED.Domain.ClassInformations;
using CED.Infrastructure.Entity_Framework_Core;

namespace CED.Infrastructure.Persistence.Repository;


public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {

    }

    public List<ClassInformation> GetLearningClassInformationsByUserId(Guid guid)
    {
        var result = Context.Set<ClassInformation>()
            .Where(x => x.LearnerId.Equals(guid))
            .ToList();
        return result;
    }

    public List<ClassInformation> GetTeachingClassInformationsByUserId(Guid guid)
    {
        var result = Context.Set<ClassInformation>()
            .Where(x => x.TutorId.Equals(guid))
            .ToList();

        return result;
    }
}

