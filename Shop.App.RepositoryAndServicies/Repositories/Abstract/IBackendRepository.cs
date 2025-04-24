using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Repositories.Abstract
{
    /// <summary>
    /// Interface for backend repositories. 
    /// Handles data storage and retrieval in CRUD context(Save addresed as Create/Update operation).
    /// </summary>
    /// <typeparam name="T"></typeparam>
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