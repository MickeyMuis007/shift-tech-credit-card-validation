using System.Threading.Tasks;
using System.Collections.Generic;

using SharedKernel.Models;

namespace SharedKernel.Interfaces
{
  public interface IRepositoryRead<TEntity, TId, TQuery> where TEntity: class, IEntity where TQuery: QueryParams
  {
    Task<PagedList<TEntity>> GetAll(TQuery query);
    Task<TEntity> GetById(TId id);
  }
}