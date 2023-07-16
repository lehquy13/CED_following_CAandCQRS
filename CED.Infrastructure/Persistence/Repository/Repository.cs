using CED.Domain.Repository;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace CED.Infrastructure.Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Domain.Common.Models.Entity<Guid>
{
    protected readonly CEDDBContext Context;

    public Repository(CEDDBContext cEdDbContext)
    {
        Context = cEdDbContext;
    }

    public void Delete(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteById(Guid id)
    {
        try
        {
            var deleteRecord = await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (deleteRecord == null) return false;
            Context.Set<TEntity>().Remove(deleteRecord);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        // throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetAllList()
    {
        try
        {
            return await Context.Set<TEntity>().ToListAsync();
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
            return Context.Set<TEntity>().AsQueryable<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        try
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
            var createdEntity = await Context.Set<TEntity>().AddAsync(entity);

            await Context.SaveChangesAsync();

            return createdEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            var updateEntity = Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
            return updateEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}