using CED.Domain.Repository;

namespace CED.Domain.ClassInformations;

public interface IClassInformationRepository : IRepository<ClassInformation>
{
    public IEnumerable<ClassInformation> GetTeachingClassInformationsByUserId(Guid guid);
    public IEnumerable<ClassInformation> GetLearningClassInformationsByUserId(Guid guid);
}

