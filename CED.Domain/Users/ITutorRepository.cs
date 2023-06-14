using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public interface ITutorRepository : IRepository<Tutor>
{
    Task<Tutor?> GetUserByEmail(string email);
    //List<Tutor> GetTutors();
}

