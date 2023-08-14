using CED.Domain.Repository;
using CED.Domain.Review;

namespace CED.Domain.Users;

public interface ITutorRepository : IRepository<Tutor>
{
    Task<Tutor?> GetUserByEmail(string email);
    Task<List<TutorFull>> GetAllsWithFullInformation();
    Task<List<TutorReview>> GetReviewsOfTutor(Guid tutorId);
    Task<bool> Register(Tutor tutor);

    //List<Tutor> GetTutors();
}

