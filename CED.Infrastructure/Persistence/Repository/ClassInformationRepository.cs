using CED.Application.Common.Persistence;
using CED.Domain.Entities.ClassInformations;

namespace CED.Infrastructure.Persistence.Repository;


public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(CEDDBContext cEDDBContext) : base(cEDDBContext)
    {
    }
}

