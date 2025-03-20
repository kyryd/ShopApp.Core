using ShopApp.Core.Models.Models.Core.Abstract;
using RepositoryAndServicies.Repositories;

namespace RepositoryAndServicies.Repositories.Abstract
{
    public interface IRepositoryFactory<T> : IDisposable where T : Entity
    {
        IFrontendRepository<T> Get(RepositoryType option);
    }
}