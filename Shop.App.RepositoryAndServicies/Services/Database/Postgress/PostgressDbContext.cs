using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Models.Client;
using System.Diagnostics;

namespace RepositoryAndServicies.Services.Database.Postgress
{
    public sealed class PostgressDbContext(DbContextOptions<PostgressDbContext> options, string? connectionSting = null) : DbContext(options)
    {
        public DbSet<Client> Cleints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(e => e.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Debug.Assert(connectionSting != null);

                optionsBuilder.UseNpgsql(connectionSting ?? "Host = localhost:5432; Username = kir; Password = 1980; Database = wpftest");
            }
        }

        public void CreateDatabase()
        {
            Database.EnsureCreated();
        }
    }

    //    var serviceProvider = new ServiceCollection()
    //        .AddDbContext<PostgressDbContext<Person>>(options =>
    //            options.UseNpgsql("YourConnectionStringHere"))
    //        .BuildServiceProvider();
    //    context.CreateDatabase();
    //public class Person : Entity
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //}
}