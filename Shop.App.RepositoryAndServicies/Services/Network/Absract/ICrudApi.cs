using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Services.Network.Absract
{
    public interface ICrudApi<T> : IDisposable where T : class, IEntity
    {
        IHttpService HttpService { get; }

        Task<ActionResult<string>> Create(T item);
        Task<ActionResult<int?>> DeleteItem(int id);
        Task<ActionResult<T?>> GetById(int id);
        Task<ActionResult<int>> GetNewId();
        Task<ActionResult<IEnumerable<T>>> GetRange();
        Task<ActionResult<string>> SaveRange(IEnumerable<T> item);
        Task<ActionResult<string>> Update(T item);
    }
}