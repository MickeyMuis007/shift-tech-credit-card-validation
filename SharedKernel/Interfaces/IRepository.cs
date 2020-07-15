using SharedKernel.Models;

namespace SharedKernel.Interfaces
{
  public interface IRepository<TEntity, TId, TQuery>: 
    IRepositoryRead<TEntity, TId, TQuery>, IRepositoryWrite<TEntity, TId> where TEntity : class, IAggregateRoot
    where TQuery: QueryParams
  {

  }
}