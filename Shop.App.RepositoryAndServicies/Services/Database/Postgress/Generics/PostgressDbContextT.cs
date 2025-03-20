using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Models.Core.Abstract;
using System.Diagnostics;

namespace RepositoryAndServicies.Services.Database.Postgress.Generics
{
    public sealed class PostgressDbContextT<T>(DbContextOptions<PostgressDbContextT<T>> options, string? connectionSting = null) : DbContext(options) where T : Entity
    {
        public DbSet<T> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().HasKey(e => e.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Debug.Assert(connectionSting != null);

                optionsBuilder.UseNpgsql(connectionSting);
            }
        }

        public void CreateDatabase()
        {
            Database.EnsureCreated();
        }
    }
}
