using Moq;
using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.BLoC.Discounts;
using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;
using ShopApp.Core.Tests.DataSources;

namespace ShopApp.Core.Tests.BLoC.Discounts
{
    [TestClass()]
    public class OrderDiscountServiceTests
    {



        [TestMethod()]
        public void Test_01_Get_Strategy_From_Provider()
        {
            var strategyProvider = MockData.MockStrategyProviderWith4PredicatesAnd2ValueSelectors();

            Assert.IsInstanceOfType<HashSet<Predicate<IDiscount>>>(strategyProvider.Strategy.Predicates);
            Assert.AreEqual(4, strategyProvider.Strategy.Predicates.Count);
        }

        [TestMethod()]
        public void Test_02_Approved_Discounts()
        {
            var strategyProvider = MockData.MockStrategyProviderWith4PredicatesAnd2ValueSelectors();
            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Value    :  {strategyProvider.Strategy.MaxPercentDiscount.Value.Value}");
            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Category :  {strategyProvider.Strategy.MaxPercentDiscount.Value.Category}");


            OrderDiscountService orderDiscountService = new OrderDiscountService(
                strategyProvider: strategyProvider,
                currency: MockData.MockCurrencyUSD(),
                converterFactory: new Mock<ICurrencyConverterFactory<CurrencyConverter>>().Object
            );

            var orderMock = new Mock<IOrder>();
            orderMock.Setup(o => o.OrderTime).Returns(DateTime.Now);
            orderMock.Setup(o => o.Discounts).Returns([.. TestData.OrderDiscounts]);
            orderMock.Setup(o => o.Product).Returns(new Mock<IProduct>().Object);
            orderMock.Setup(o => o.NumberOfUnits).Returns(1);
            orderMock.Setup(o => o.DeliveryState).Returns(DeliveryState.Pending);
            orderMock.Setup(o => o.PaymentState).Returns(PaymentState.Pending);

            var result = orderDiscountService.ApprovedDiscounts(orderMock.Object);

            Assert.IsNotNull(result);
            Console.WriteLine($"Approved discounts: {result.Count}");
            foreach (var item in result)
            {
                Console.WriteLine($"Discounts: {item}");
            }

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod()]
        public void Test_03_Aggregated_Discount_For_Order()
        {
            //Arrange
            var strategyProvider = MockData.MockStrategyProviderWith4PredicatesAnd2ValueSelectors();
            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Value    :  {strategyProvider.Strategy.MaxPercentDiscount.Value.Value}");
            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Category :  {strategyProvider.Strategy.MaxPercentDiscount.Value.Category}");

            OrderDiscountService orderDiscountService = new OrderDiscountService(
                strategyProvider: strategyProvider,
                currency: MockData.MockCurrencyUSD(),
                converterFactory: new Mock<ICurrencyConverterFactory<CurrencyConverter>>().Object
            );

            var orderMock = new Mock<IOrder>();
            orderMock.Setup(o => o.OrderTime).Returns(DateTime.Now);
            orderMock.Setup(o => o.Discounts).Returns([.. TestData.OrderDiscounts]);
            orderMock.Setup(o => o.Product).Returns(new Mock<IProduct>().Object);
            orderMock.Setup(o => o.NumberOfUnits).Returns(1);
            orderMock.Setup(o => o.DeliveryState).Returns(DeliveryState.Pending);
            orderMock.Setup(o => o.PaymentState).Returns(PaymentState.Pending);


            //Act
            var aggregatedDiscount = orderDiscountService.AggregatedDiscount(orderMock.Object);


            //Assert
            Assert.IsNotNull(aggregatedDiscount);
            //To console every field of aggregatedDisount
            Console.WriteLine($"aggregatedDisount : Discount Type: {aggregatedDiscount.DisountType}");
            Console.WriteLine($"aggregatedDisount : Valid From: {aggregatedDiscount.ValidFrom}");
            Console.WriteLine($"aggregatedDisount: Valid To: {aggregatedDiscount.ValidTo}");
            Console.WriteLine($"aggregatedDisount : Value: {aggregatedDiscount.Value}");


            //Expected 
            //DiscountType.ForVip 7%
            //DiscountType.WhenHappyHour 15%
            //15 + 7 = 22% > MaxPercentDiscount = 20%
            //Expected: MaxPercentDiscount = 20%
            Assert.AreEqual(20m, aggregatedDiscount.Value.Value);
        }


    }
}