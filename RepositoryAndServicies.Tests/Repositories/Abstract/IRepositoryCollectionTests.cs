using Moq;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Address;
using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Baskets;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client;
using ShopApp.Core.Models.Models.Core.Abstract;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RepositoryAndServicies.Repositories.Abstract.Tests
{
    [TestClass()]
    public class IRepositoryCollectionTests
    {
        private static ICurrency MockCurrencyPLN()
        {
            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.PLN);
            return mockCurrency.Object;
        }

        private static ICurrency MockCurrencyUSD()
        {

            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.USD);
            return mockCurrency.Object;
        }

        private Client GetClientWithId()
        {
            return new Client
            (
                FirstName: "Pol",
                LastName: "Andersen",
                ClientAddress: new Address(Street: "123 Main St", City: "Springfield", State: "IL", Zip: "62701", Country: "USA"),
                DeliveryAddresses: new List<IAddress>
                {
                        new Address("456 Elm St", "Springfield", "IL", "62702", "USA"),
                        new Address("789 Oak St", "Springfield", "IL", "62703", "USA")
                },
                IsVip: true
            )
            { Id = 1 };
        }

        private Client GetClientNoId()
        {
            return new Client
            (
                FirstName: "Pol",
                LastName: "Andersen",
                ClientAddress: new Address(Street: "123 Main St", City: "Springfield", State: "IL", Zip: "62701", Country: "USA"),
                DeliveryAddresses: new List<IAddress>
                {
                        new Address("456 Elm St", "Springfield", "IL", "62702", "USA"),
                        new Address("789 Oak St", "Springfield", "IL", "62703", "USA")
                },
                IsVip: true
            );
        }

        private IList<IBasketItem> GetBasketItems(ICurrency currency) =>
          [
              new BasketItem
                            (
                                ProductId: 1,
                                Quantity: 2,
                                Date: new DateTime(2025, 2, 1),
                                Price: new Price(currency, 100m){ Id = 1 }
                            ){ Id = 1 },
             new BasketItem
                            (
                                ProductId: 2,
                                Quantity: 1,
                                Date: new DateTime(2025, 2, 1),
                                Price: new Price(currency, 50m)
                            ) { Id = 2 }
          ];

        [TestMethod()]
        public async Task Test_01_Create_Repository_Collection()
        {
            // Arrange
                var currency = MockCurrencyPLN();

                var repositoryCollectionMock = new Mock<IRepositoryCollection>();
                var repositories = new ConcurrentDictionary<Type, IFrontendRepository<Entity>>();
                repositoryCollectionMock.Setup(r => r.Repositories).Returns(repositories);

                var repositoryCollection = repositoryCollectionMock.Object;
            
            ///////////////////////////////Basket
            
                var basketResponse = new Mock<IResponse<Entity?>>();
                basketResponse.Setup(r => r.Value).Returns(new Basket(Consumer: GetClientNoId(), Items: []));
                basketResponse.Setup(r => r.IsError).Returns(false);

                var basketRepositoryMock = new Mock<IFrontendRepository<Entity>>();
                basketRepositoryMock.Setup(r => r.SaveAsync(It.IsAny<Basket>())).ReturnsAsync(basketResponse.Object!);
                basketRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(basketResponse.Object!);

            // Act
                repositoryCollection.Repositories.TryAdd(typeof(Basket), basketRepositoryMock.Object!);
                repositoryCollection.Repositories.TryGetValue(typeof(Basket), out var basketRepository);

            // Assert
                Assert.IsNotNull(basketRepository);
                Assert.IsInstanceOfType<IFrontendRepository<Entity>>(basketRepository);
                //Assert.IsInstanceOfType<IFrontendRepository<Basket>>(basketRepository);
                Assert.AreEqual(1, repositoryCollection.Repositories.Count);

            // Act
                var saveResponse = await basketRepository!.SaveAsync(new Basket(GetClientWithId(), []));
                Assert.IsNotNull(saveResponse);
                Assert.IsInstanceOfType<Basket>(saveResponse.Value);
        }

    }
}