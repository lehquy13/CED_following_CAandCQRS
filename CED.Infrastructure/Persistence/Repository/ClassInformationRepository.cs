using CED.Domain.ClassInformations;

namespace CED.Infrastructure.Persistence.Repository;


public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }
}

