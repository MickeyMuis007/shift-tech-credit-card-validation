using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
  public interface IRepositoryWrite<TEntity, TId> where TEntity : class, IAggregateRoot
  {
    Task<TEntity> Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity id);
  }
}