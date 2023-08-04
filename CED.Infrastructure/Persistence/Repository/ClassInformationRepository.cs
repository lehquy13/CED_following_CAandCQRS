using CED.Domain.ClassInformations;
using CED.Domain.Review;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class ClassInformationRepository : Repository<ClassInformation>, IClassInformationRepository
{
    public ClassInformationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<ClassInformation?> GetById(Guid id)
    {
        var result = await _appDbContext.ClassInformations
            .Where(x => x.Id == id)
            .Include(x => x.RequestGettingClasses)
            .Include(x => x.TutorReviews)
            .Include(x => x.Subject)
            .Include(x => x.Tutor)
            .Include(x => x.Learner)
            .SingleOrDefaultAsync();
        return result;
    }

    public async Task<List<ClassInformation>> GetAllTeachingClassesOfTutor(Guid tutorId)
    {
        var result = await _appDbContext.ClassInformations
            .Where(x => x.TutorId == tutorId && x.Status == Status.Confirmed)
            .Include(x => x.TutorReviews)
            .OrderByDescending(x => x.CreationTime)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        return result;
    }

    public async Task<List<ClassInformation>> GetLearningClassInformationsByUserId(Guid learnerId)
    {
        var result = await _appDbContext.Set<ClassInformation>()
            .Where(x => x.LearnerId == learnerId && x.IsDeleted == false)
            .Include(x => x.Subject)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
        return result;
    }

    public async Task<List<RequestGettingClass>> GetRequestGettingClassesByClassId(Guid classId)
    {
        var requestList = await _appDbContext.Set<RequestGettingClass>()
            .Where(x => x.ClassInformationId.Equals(classId))
            .ToListAsync();
        return requestList;
    }

    public async Task<List<RequestGettingClass>> GetAllClassRequestsByUserId(Guid tutorId)
    {
        var result = await _appDbContext.Set<RequestGettingClass>()
            .Where(x => x.TutorId == tutorId)
            .Include(x => x.Tutor)
            .ToListAsync();
        return result;
    }

    public async Task<ClassInformation?> GetAllClassWithRequest(Guid classId)
    {
        var result = await _appDbContext.Set<ClassInformation>()
            .Where(x => x.Id == classId)
            .Include(x => x.RequestGettingClasses)
            .ThenInclude(x => x.Tutor)
            .SingleOrDefaultAsync();

        return result;
    }

    public async Task<TutorReview?> GetReviewByClassId(Guid classId)
    {
        var result = await _appDbContext.TutorReviews.SingleOrDefaultAsync(x => x.ClassInformationId == classId);
        return result;
    }
}