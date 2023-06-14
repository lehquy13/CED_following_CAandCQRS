using CED.Domain.Repository;

namespace CED.Domain.ClassInformations;

public interface IClassInformationRepository : IRepository<ClassInformation>
{
    public List<ClassInformation> GetTeachingClassInformationsByUserId(Guid guid);
    public List<ClassInformation> GetLearningClassInformationsByUserId(Guid guid);
}

