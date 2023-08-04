using CED.Domain.Repository;

namespace CED.Domain.Subjects;

public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetSubjectByName(string name);
    public Task<List<Subject>> GetTutorMajors(Guid tutorId);
}

