using ShopApp.Core.Models.Models.Core.Abstract;
using System.Collections.Concurrent;

namespace RepositoryAndServicies.Repositories.Abstract
{
    public interface IRepositoryCollection : IDisposable  
    {
        ConcurrentDictionary<Type, IFrontendRepository<Entity>> Repositories { get; }

        public Task<IFrontendRepository<Entity>> AddAsync(IFrontendRepository<Entity> repository);
    }
}
