using System.Threading.Tasks;

using SharedKernel.Models;

namespace SharedKernel.Interfaces {
  public interface IService <TViewModel, TId, TQuery> where TViewModel : class, IDTO where TQuery : QueryParams {
    Task<PagedList<TViewModel>> Get(TQuery query);
    Task<TViewModel> GetById(TId id);
    Task<TViewModel> Insert(TViewModel model);
    Task<TViewModel> Update(TId id, TViewModel model);
    Task<TViewModel> Delete(TId id);
  }

  public interface IService <TViewModel, TUpdateModel, TId, TQuery>
    where TViewModel : class, IDTO
    where TUpdateModel : class, IUpdateDTO 
    where TQuery : QueryParams {
    Task<PagedList<TViewModel>> Get(TQuery query);
    Task<TViewModel> GetById(TId id);
    Task<TViewModel> Insert(TViewModel model);
    Task<TViewModel> Update(TId id, TUpdateModel model);
    Task<TViewModel> Delete(TId id);
  }

  public interface IService <TDTO, TCreateModel, TUpdateModel, TId, TQuery>
    where TDTO : class, IDTO
    where TCreateModel : class, ICreateDTO
    where TUpdateModel : class, IUpdateDTO
    where TQuery : QueryParams {
      Task<PagedList<TDTO>> Get(TQuery query);
      Task<TDTO> GetById(TId id);
      Task<TDTO> Insert(TCreateModel model);
      Task<TDTO> Update(TId id, TUpdateModel model);
      Task<TDTO> Delete(TId id);
    }
}