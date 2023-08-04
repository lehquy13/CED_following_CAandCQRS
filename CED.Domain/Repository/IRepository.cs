using CED.Domain.Common.Models;

namespace CED.Domain.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity<Guid>
{
    //Queries
    /// <summary>
    /// Get all the record of tables into a list of object
    /// </summary>
    Task<List<TEntity>> GetAllList();

    /// <summary>
    /// Get all the record of tables and able to query with linq due to the iqueryable<> return
    /// </summary>
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetById(Guid id);

    //Insert
    Task<TEntity> Insert(TEntity entity);

    //Update
    TEntity? Update(TEntity entity);

    //Remove
    void Delete(TEntity entity);

    Task<bool> DeleteById(Guid id);
    Task SaveAll();    
    Task<TEntity?> ExistenceCheck(Guid id);

}