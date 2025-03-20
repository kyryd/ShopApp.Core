using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Repositories.Abstract
{

    public interface IBackendRepository<T> : IFrontendRepository<T> where T : Entity
    {
        IResponse<int> GetNewId();

        IResponse<T?> Add(T entity);
        IResponse<T?> Update(T entity);
        Task<IResponse<T?>> AddAsync(T entity);

        Task<IResponse<T?>> UpdateAsync(T entity);

        Task<IResponse<IEnumerable<T>>> UpdateAllAsync(IEnumerable<T> entities);

        Task<IResponse<IEnumerable<T>>> AddAllAsync(IEnumerable<T> entities);

    }
}