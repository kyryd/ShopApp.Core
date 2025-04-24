using Moq;
using RepositoryAndServicies.Repositories.Abstract;
using ShopApp.Core.Logic.BLoC.Baskets;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Address;
using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Baskets;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client;

namespace ShopApp.Core.Logic.Tests.BLoC.Baskets
{
    [TestClass()]
    public class BasketServiceTests
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

        [TestMethod()]
        public async Task Test_01_Create_Basket()
        {
            // Arrange
            var currency = MockCurrencyPLN();

            var response = new Mock<IResponse<Basket?>>();
            response.Setup(r => r.Value).Returns(new Basket(Consumer: GetClientNoId(), Items: []));
            response.Setup(r => r.IsError).Returns(false);

            var frontendRepository = new Mock<IFrontendRepository<Basket>>();
            frontendRepository.Setup(r => r.SaveAsync(It.IsAny<Basket>())).ReturnsAsync(response.Object!);

            var basketService = new BasketService(frontendRepository.Object);

            // Act

            var result = await basketService.CreateBasket(GetClientNoId());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod()]
        public async Task Test_02_Remove_Basket()
        {
            // Arrange
            var currency = MockCurrencyPLN();

            var response = new Mock<IResponse<Basket?>>();
            response.Setup(r => r.Value).Returns(new Basket(Consumer: GetClientWithId(), Items: []));
            response.Setup(r => r.IsError).Returns(false);

            var frontendRepository = new Mock<IFrontendRepository<Basket>>();
            frontendRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(response.Object!);

            var basketService = new BasketService(frontendRepository.Object);

            // Act

            var result = await basketService.RemoveBasket(GetClientWithId());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);
        }

        [TestMethod()]
        public async Task Test_03_Remove_Basket_No_Id()
        {
            // Arrange
            var currency = MockCurrencyPLN();


            var response = new Mock<IResponse<Basket?>>();
            response.Setup(r => r.Value).Returns(new Basket(Consumer: GetClientNoId(), Items: []));
            response.Setup(r => r.IsError).Returns(false);

            var frontendRepository = new Mock<IFrontendRepository<Basket>>();
            frontendRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(response.Object!);

            var basketService = new BasketService(frontendRepository.Object);

            // Act

            var result = await basketService.RemoveBasket(GetClientNoId());

            // Assert
            Assert.IsNull(result);
        }


    }
}