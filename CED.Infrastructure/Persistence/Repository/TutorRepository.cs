using CED.Domain.Review;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class TutorRepository : Repository<Tutor>, ITutorRepository
{
    public TutorRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override Task<Tutor?> GetById(Guid id)
    {
        try
        {
            return _appDbContext.Tutors
                .Where( o => o.Id == id && o.IsDeleted == false)
                .Include(x => x.Subjects)
                .Include(x => x.TutorVerificationInfos)
                .Include(x => x.RequestGettingClasses)
                .FirstOrDefaultAsync();
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }


    public async Task<Tutor?> GetUserByEmail(string email)
    {
        try
        {
            return await _appDbContext.Set<Tutor>().FirstOrDefaultAsync(o => o.Email == email);
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<TutorFull>> GetAllsWithFullInformation()
    {
        try
        {
            var user = await _appDbContext.Set<User>()
                .Where(x => x.IsDeleted == false)
                .Join(
                    _appDbContext.Set<Tutor>(),
                    u => u.Id,
                    tu => tu.Id,
                    (u,tu) => 
                    new TutorFull(u,tu)
                ).ToListAsync();
            return user;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
    }

   
    public async Task<List<TutorReview>> GetReviewsOfTutor(Guid tutorId)
    {
        var result = await _appDbContext.ClassInformations
            .Where(x => x.TutorId == tutorId && x.Status == Status.Confirmed && x.IsDeleted == false)
            .Include(x => x.TutorReviews)
            .Select(x => x.TutorReviews)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
        return result;
    }

    /// <summary>
    /// Deprecated
    /// </summary>
    /// <param name="tutor"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> Register(Tutor tutor)
    {
        try
        {
            await _appDbContext.Tutors.AddAsync(tutor);
            //mark that this record is already create in User table and we are adding it to Tutor table
            _appDbContext.Entry(tutor).State = EntityState.Modified;
            //await _appDbContext.Users.AddAsync(tutor);    
            return true;
        }
        catch(Exception ex) { 
            throw new Exception(ex.Message);
        }
        return true;
    }

    
}

