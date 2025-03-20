using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Services.Database
{
    public interface IContextProvider<T>: IDisposable where T :  Entity
    {
        DbContext Context { get; }
        DbSet<T> DbSet { get; }

        void InitContext();
    }
}