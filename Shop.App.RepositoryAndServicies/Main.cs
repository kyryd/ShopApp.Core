using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryAndServicies.Services.Database.Postgress;
using RepositoryAndServicies.Services.Database.Postgress.Generics;
using ShopApp.Core.Models.Models.Address;
using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Client;
using ShopApp.Core.Models.Models.Client.Abstract;

namespace RepositoryAndServicies
{
    class Program
    {
        static void Main(string[] args)
        {
            //!!test1();-OK
            //!!test2(); GENERICS -OK
            //test3(); //GENERICS -OK
            //!!ConfigureServices();


        }

        internal static readonly IList<IClient> Clients =
        [
            new Client
            (
                FirstName:"Pol",
                LastName:"Andersen",
                ClientAddress: new Address(Street: "123 Main St", City: "Springfield", State: "IL", Zip: "62701", Country: "USA"),
                DeliveryAddresses: new List<IAddress>
                {
                    new Address("456 Elm St", "Springfield", "IL", "62702", "USA"),
                    new Address("789 Oak St", "Springfield", "IL", "62703", "USA")
                },
                IsVip: true
            ),
            new Client
            (
                FirstName:"Matthew",
                LastName:"McConaughey",
                ClientAddress: new Address(Street: "321 Maple St", City: "Springfield", State: "IL", Zip: "62704", Country: "USA"),
                DeliveryAddresses: new List<IAddress>
                {
                    new Address("654 Pine St", "Springfield", "IL", "62705", "USA"),
                    new Address("987 Birch St", "Springfield", "IL", "62706", "USA")
                },
                IsVip: false
            ),
            new Client
            (
                FirstName:"Arnold",
                LastName:"Schwarzenegger",
                ClientAddress: new Address(Street: "111 Oak St", City: "Springfield", State: "IL", Zip: "62707", Country: "USA"),
                DeliveryAddresses: new List<IAddress>
                {
                    new Address("222 Cedar St", "Springfield", "IL", "62708", "USA"),
                    new Address("333 Walnut St", "Springfield", "IL", "62709", "USA")
                },
                IsVip: true
            )
        ];
        static void test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgressDbContext>();
            optionsBuilder.UseNpgsql("Host = localhost:5432; Username = kir; Password = 1980; Database = wpftest");

            using (var context = new PostgressDbContext(optionsBuilder.Options))
            {
                try
                {
                    context?.Database.EnsureCreated();
                    context!.Database.OpenConnection();
                    Console.WriteLine("Connection successful!");
                    context!.Clients.Add((Client)Clients[0]);
                    context!.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection failed: {ex.Message}");
                }
            }
        }

        //static void test2()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<PostgressDbContextT<Person>>();
        //    optionsBuilder.UseNpgsql("Host = localhost:5432; Username = kir; Password = 1980; Database = wpftest");


        //    using (var context = new PostgressDbContextT<Person>(optionsBuilder.Options))
        //    {
        //        try
        //        {

        //            context?.Database.EnsureCreated();
        //            context!.Database.OpenConnection();
        //            Console.WriteLine("Connection successful!");
        //            context!.Entities.Add(new Person("IOP", "SOP", 1980));
        //            context!.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Connection failed: {ex.Message}");
        //        }
        //    }
        //}
        ////static void test3()
        ////{
        ////    var optionsBuilder = new DbContextOptionsBuilder<PostgressDbContextT<Person>>();
        ////    optionsBuilder.UseNpgsql("Host = localhost:5432; Username = kir; Password = 1980; Database = wpftest");



        ////    using (var context = new PostgressDbContextT<Person>(optionsBuilder.Options))
        ////    {
        ////        try
        ////        {
        ////            IFrontendRepository<Person> frontendRepository = new EfRepository<Person>(context);
        ////            frontendRepository.Save(new Person("IOP", "SOP", 1980));
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            Console.WriteLine($"Connection failed: {ex.Message}");
        ////        }
        ////    }
        ////}

        private static ServiceProvider ConfigureServices()
        {
            // Register DbContext

            var serviceCollection = new ServiceCollection();



            serviceCollection.AddDbContext<PostgressDbContext>(optionsBuilder =>
            optionsBuilder.UseNpgsql("Host = localhost:5432; Username = kir; Password = 1980; Database = wpftest"));
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using var context = serviceProvider.GetService<PostgressDbContext>();


            try
            {
                context?.Database.EnsureCreated();
                context!.Database.OpenConnection();
                Console.WriteLine("Connection successful!");
                context!.Clients.Add((Client)Clients[0]);
                context!.SaveChanges();

                var allPersons = context.Clients.ToList(); // Retrieve all Person objects
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
            return serviceProvider;
        }
    }
}