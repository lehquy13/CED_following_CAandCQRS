using CED.Domain.Repository;

namespace CED.Domain.ClassInformations;

public interface IRequestGettingClassRepository : IRepository<RequestGettingClass>
{
    Task<List<RequestGettingClass>> GetAllRequestGettingClass(Guid classId);
    Task<bool> IsRequested(Guid tutorId, Guid classId);
}