using Microsoft.EntityFrameworkCore;
using RepositoryAndServicies.Repositories.Abstract;
using RepositoryAndServicies.Services.Database;
using RepositoryAndServicies.Services.Database.Postgress.Generics;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Repositories
{
    public enum RepositoryType
    {
        LocalInMemory = 1,
        LocalDB = 2,
        LocalNetworkService = 3,
        RemoteNetworkService = 4,
    }

    public sealed class RepositoryFactory<T> : IRepositoryFactory<T> where T : Entity
    {
        public string LocalServerAddress = string.Empty;
        public string RemoteServerAddress = string.Empty;
        public ContextProvider<T>? ContextProvider { get; set; }
        public string? DbConnectionString { get; set; }
        private DbContextOptionsBuilder<PostgressDbContextT<T>> OptionsBuilderForPostgress { get; set; }


        public RepositoryFactory(string localServerAddress,
                              string remoteServerAddress,
                              string dbConnectionString)
        {
            LocalServerAddress = localServerAddress;
            RemoteServerAddress = remoteServerAddress;
            DbConnectionString = dbConnectionString;

            OptionsBuilderForPostgress = new();
            ContextProvider = new(() => new PostgressDbContextT<T>(OptionsBuilderForPostgress.UseNpgsql(DbConnectionString).Options));
            _repositories = new()
            {
                    { RepositoryType.LocalInMemory, new InMemoryRepository<T>() },
                    { RepositoryType.LocalDB, new EfRepository <T>(ContextProvider) },
                    { RepositoryType.LocalNetworkService, new NetworkRepository <T>(LocalServerAddress) },
                    { RepositoryType.RemoteNetworkService, new NetworkRepository <T>(RemoteServerAddress) },
             };
        }

        private readonly Dictionary<RepositoryType, IFrontendRepository<T>> _repositories;

        private readonly Dictionary<RepositoryType, Func<RepositoryType, IFrontendRepository<T>>> _factories;


        public IFrontendRepository<T> Get(RepositoryType option) => _repositories[option]; 

        public void Dispose()
        {
            _repositories.Values.ToList().ForEach(factory => factory?.Dispose());
        }
    }
}
