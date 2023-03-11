using CED.Domain.Entities.Subject;
using CED.Domain.Entities.User;

namespace CED.Application.Common.Persistence;

public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetSubjectByName(string name);
}

