using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SharedKernel.Interfaces;
using SharedKernel.Models;
using Persistence.Contexts;

namespace Infrastructure.Implementations.Learnex.Repositories {
  public abstract class Repository<TEntity, TId, TQuery> : IAggregateRoot where TEntity : class where TQuery : QueryParams{
     protected readonly CMSDbContexts _db;

    public Repository(CMSDbContexts db) {
      _db = db;
    }

    public async Task<IEnumerable<TEntity>> GetAll(TQuery query) {
      return await _db.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(TId id) {
      return await _db.FindAsync<TEntity>(id);
    }

    public async Task<TEntity> Insert(TEntity entity) {
      var results = await _db.AddAsync(entity);
      return entity;
    }

    public void Update(TEntity entity) {
      _db.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity) {
      _db.Remove(entity);
    }
  }
}