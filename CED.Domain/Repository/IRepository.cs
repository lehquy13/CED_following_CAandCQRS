﻿using CED.Domain.Common.Models;

namespace CED.Domain.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity<Guid>
{
    //Queries
    /// <summary>
    /// Get all the record of tables into a list of object
    /// </summary>
    public Task<List<TEntity>> GetAllList();

    /// <summary>
    /// Get all the record of tables and able to query with linq due to the iqueryable<> return
    /// </summary>
    public IQueryable<TEntity> GetAll();

    public Task<TEntity> GetById(Guid id);

    //Insert
    public Task<TEntity> Insert(TEntity entity);

    //Update
    public TEntity? Update(TEntity entity);

    //Remove
    public void Delete(TEntity entity);

    public Task<bool> DeleteById(Guid id);


}

