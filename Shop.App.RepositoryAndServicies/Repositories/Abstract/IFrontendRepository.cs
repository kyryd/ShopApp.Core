using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Repositories.Abstract
{

    /// <summary>
    /// IFrontendRepository interface for Get/Save/Delete operations on entities in terms of domain logic.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFrontendRepository<T>:IDisposable where T : Entity
    {
        
        IResponse<T?> Get(int id);
        IResponse<IEnumerable<T>> GetAll();
        IResponse<T?> Delete(int id);

        Task<IResponse<T?>> SaveAsync(T entity);
        IResponse<T?> Save(T entity);

        Task<IResponse<IEnumerable<T>>> SaveAllAsync(IEnumerable<T> entities);

        Task<IResponse<T?>> GetAsync(int id);

        Task<IResponse<IEnumerable<T>>> GetAllAsync();
        Task<IResponse<T?>> DeleteAsync(int id);

        Task<IResponse<bool>> IsEmpty();

        Task<IResponse<IEnumerable<T>>> DeleteAllAsync(IEnumerable<T> entities);

        IResponse<bool> Exists(T entity);
    }
}