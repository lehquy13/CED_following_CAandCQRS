using CED.Domain.Repository;

namespace CED.Domain.Subjects;

public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetSubjectByName(string name);
}

