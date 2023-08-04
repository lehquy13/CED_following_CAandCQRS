using CED.Domain.Repository;
using CED.Domain.Review;

namespace CED.Domain.ClassInformations;

public interface IClassInformationRepository : IRepository<ClassInformation>
{
    Task<List<RequestGettingClass>> GetAllClassRequestsByUserId(Guid tutorId);
    Task<List<ClassInformation>> GetAllTeachingClassesOfTutor (Guid tutorId);
    Task<List<ClassInformation>> GetLearningClassInformationsByUserId(Guid learnerId);
    Task<List<RequestGettingClass>> GetRequestGettingClassesByClassId(Guid classId);
    Task<ClassInformation?> GetAllClassWithRequest(Guid classId);
    
    //Reviews of Tutor
    Task<TutorReview?> GetReviewByClassId(Guid classId);
}