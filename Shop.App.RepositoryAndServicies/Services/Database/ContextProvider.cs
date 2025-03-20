using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies.Services.Database
{
    public sealed class ContextProvider<T> : IContextProvider<T> where T : Entity
    {
        private readonly Func<DbContext> contextBuilder;
        private DbContext? _localDbContext;

        public ContextProvider(Func<DbContext> contextBuilder)
        {
            this.contextBuilder = contextBuilder;
            InitContext();
        }

        public DbContext Context => _localDbContext ??= contextBuilder();
        public DbSet<T> DbSet => (_localDbContext ??= contextBuilder()).Set<T>();

        public void Dispose()
        {
            _localDbContext?.Dispose();
        }

        public void InitContext()
        {
            _localDbContext?.Dispose();
            _localDbContext = contextBuilder();
        }
    }
}
