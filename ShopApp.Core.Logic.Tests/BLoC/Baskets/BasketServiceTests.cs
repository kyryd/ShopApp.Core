using Moq;
using RepositoryAndServicies.Repositories.Abstract;
using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Address;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Baskets;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using System;
using System.Threading.Tasks;

namespace ShopApp.Core.Logic.BLoC.Baskets.Tests
{
    [TestClass()]
    public class BasketServiceTests
    {

        public static ICurrency MockCurrencyPLN()
        {
            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.PLN);
            return mockCurrency.Object;
        }

        public static ICurrency MockCurrencyUSD()
        {

            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.USD);
            return mockCurrency.Object;
        }
        [TestMethod()]
        public async Task Test_01_Create_Basket()
        {
            // Arrange
                var currency = MockCurrencyPLN();

                var client = new Client
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

                IList<IBasketItem> basketItems =
                [
                    new BasketItem
                    (
                        ProductId: 1,
                        Quantity: 2,
                        Date: new DateTime(2025, 2, 1),
                        Price: new Price(currency, 100m)
                    ),
                    new BasketItem
                    (
                        ProductId: 2,
                        Quantity: 1,
                        Date: new DateTime(2025, 2, 1),
                        Price: new Price(currency, 50m)
                    )
                ];

                var response = new Mock<IResponse<Basket?>>();
                response.Setup(r => r.Value).Returns(new Basket(Consumer: client, Items: []));
                response.Setup(r => r.IsError).Returns(false);
                //_mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(response.Object!);
                //_mockRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))!.ReturnsAsync(response.Object!);

                var frontendRepository = new Mock<IFrontendRepository<Basket>>();
                frontendRepository.Setup( r => r.SaveAsync(It.IsAny<Basket>())).ReturnsAsync(response.Object!);

                var basketService = new BasketService(frontendRepository.Object);
            
            // Act

                var result = await basketService.CreateBasket(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod()]
        public async Task Test_02_Remove_Basket()
        {
            // Arrange
            var currency = MockCurrencyPLN();

            var client = new Client
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
            { Id = 1};

            var response = new Mock<IResponse<Basket?>>();
            response.Setup(r => r.Value).Returns(new Basket(Consumer: client, Items: []));
            response.Setup(r => r.IsError).Returns(false);
            //_mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(response.Object!);
            //_mockRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))!.ReturnsAsync(response.Object!);

            var frontendRepository = new Mock<IFrontendRepository<Basket>>();
            frontendRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(response.Object!);

            var basketService = new BasketService(frontendRepository.Object);

            // Act

            var result = await basketService.RemoveBasket(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod()]
        public async Task Test_03_Remove_Basket_No_Id()
        {
            // Arrange
            var currency = MockCurrencyPLN();

            var client = new Client
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
            

            var response = new Mock<IResponse<Basket?>>();
            response.Setup(r => r.Value).Returns(new Basket(Consumer: client, Items: []));
            response.Setup(r => r.IsError).Returns(false);
            //_mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(response.Object!);
            //_mockRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))!.ReturnsAsync(response.Object!);

            var frontendRepository = new Mock<IFrontendRepository<Basket>>();
            frontendRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(response.Object!);

            var basketService = new BasketService(frontendRepository.Object);

            // Act

            var result = await basketService.RemoveBasket(client);

            // Assert
            Assert.IsNull(result);
        }
    }
}