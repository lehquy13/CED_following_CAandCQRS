using CED.Domain.Repository;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Domain.Common.Models.Entity<Guid>
{
    protected readonly AppDbContext _appDbContext;

    public Repository(AppDbContext cEdDbAppDbContext)
    {
        _appDbContext = cEdDbAppDbContext;
    }

    public void Delete(TEntity entity)
    {
        try
        {
            _appDbContext.Set<TEntity>().Remove(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Auto save changes but it is a deprecated method
    /// Remember to use non tracking record
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteById(Guid id)
    {
        try
        {
            var deleteRecord = await _appDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (deleteRecord == null) return false;
            _appDbContext.Set<TEntity>().Remove(deleteRecord);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task SaveAll()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> ExistenceCheck(Guid id)
    {
        try
        {
            return await _appDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }

    public virtual async Task<List<TEntity>> GetAllList()
    {
        try
        {
            return await _appDbContext.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return _appDbContext.Set<TEntity>().AsQueryable<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        try
        {
            return await _appDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        try
        {
            var createdEntity = await _appDbContext.Set<TEntity>().AddAsync(entity);
            return createdEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Deprecated method
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public TEntity Update(TEntity entity)
    {
        try
        {
            var updateEntity = _appDbContext.Set<TEntity>().Update(entity);
            _appDbContext.SaveChanges();
            return updateEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}