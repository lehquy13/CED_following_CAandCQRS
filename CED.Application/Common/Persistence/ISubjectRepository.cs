using CED.Domain.Entities.Subjects;

namespace CED.Application.Common.Persistence;

public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetSubjectByName(string name);
}

