using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Discounts;
using ShopApp.Core.Logic.BLoC.Inventory.Provider;
using ShopApp.Core.Logic.BLoC.Inventory.Provider.Abstract;
using ShopApp.Core.Logic.BLoC.Inventory.Service;
using ShopApp.Core.Logic.BLoC.Inventory.Service.Abstract;
using ShopApp.Core.Logic.BLoC.Orders;
using ShopApp.Core.Logic.BLoC.Orders.Abstract;
using ShopApp.Core.Logic.BLoC.Payment;
using ShopApp.Core.Logic.BLoC.Payment.Service;
using ShopApp.Core.Logic.BLoC.Prices;
using ShopApp.Core.Logic.BLoC.Prices.Abstract;
using ShopApp.Core.Logic.BLoC.Shipment.Service;
using ShopApp.Core.Logic.BLoC.Shipment.Service.Abstract;
using ShopApp.Core.Logic.BLoC.Taxes;
using ShopApp.Core.Logic.BLoC.Taxes.Abstract;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Tests.DataSources;

namespace ShopApp.Core.Tests.BLoC
{
    [TestClass()]
    public class OrderProviderTests
    {

        private static (IInventoryService<IOrder, IProduct> stockService,
                IShipmentService<IOrder, IProduct> shipmentService,
                IPaymentService paymentService,
                CurrencyConverterFactory<CurrencyConverter> currencyConverterFactory,
                ITaxProvider taxProvider,
                IPriceCaluclator<IPrice> priceCalculator,
                OrderDiscountService discountService)
            GetServices(ICurrency targetCurrency)
        {


            IInventoryProvider<IProduct> stockProvider = new InventoryProviderMock();
            IInventoryService<IOrder, IProduct> stockService = new InventoryService(stockProvider: stockProvider);
            IShipmentService<IOrder, IProduct> shipmentService = new ShipmentServiceMock<IOrder, IProduct>();
            IPaymentService paymentService = new PaymentServiceMock();
            CurrencyConverterFactory<CurrencyConverter> currencyConverterFactory = new CurrencyConverterFactory<CurrencyConverter>(DataSources.TestData.ExchangeRates);
            IPriceCaluclator<IPrice> priceCalculator = (IPriceCaluclator<IPrice>)new PriceCalculator(currencyConverterFactory);
            ITaxProvider taxProvider = new TaxProviderMock();

            var strategyProvider = MockData.MockStrategyProviderWith4PredicatesAnd2ValueSelectors();

            OrderDiscountService orderDiscountService = new OrderDiscountService(
                strategyProvider: strategyProvider,
                currency: targetCurrency,
                converterFactory: currencyConverterFactory
            );

            return (stockService, shipmentService, paymentService, currencyConverterFactory, taxProvider, priceCalculator, orderDiscountService);
        }




        [TestInitialize]
        public void TestInitialize()
        {



        }



        public void Test_Get_Total_in_PLN(IOrder order)
        {
            // Arrange
            Console.WriteLine();
            Console.WriteLine("==========================================================================================");
            Console.WriteLine();

            ICurrency targetCurrency = DataSources.TestData.CurrenciesList.First(c => c.Value == Core.Models.Enums.Currencies.PLN);
            Console.WriteLine($"Target currency: {targetCurrency.Value}");

            var services = GetServices(targetCurrency);

            var strategy = services.discountService.StrategyProvider.Strategy;
            Console.WriteLine($"MaxPercentDiscount.Value.Value    :  {strategy.MaxPercentDiscount.Value.Value}");
            Console.WriteLine($"MaxPercentDiscount.Value.Category :  {strategy.MaxPercentDiscount.Value.Category}");



            Console.WriteLine("Order details:");

            Console.WriteLine($"\tOrder: {nameof(order.Product.Name)}:{order.Product.Name}, " +
                                     $"{nameof(order.Product.Unit)}:{order.Product.Unit}, " +
                                     $"{nameof(order.NumberOfUnits)}:{order.NumberOfUnits}, " +
                                     $"{nameof(order.Product.PricePerUnit)}:{order.Product.PricePerUnit.Amount}, " +
                                     $"{nameof(order.Product.PricePerUnit.Currency)}:{order.Product.PricePerUnit.Currency.Value}, \n");

            Console.WriteLine($"\tDiscounts: {nameof(order.Discounts.Count)}:{order.Discounts.Count}");

            foreach (var discount in order.Discounts)
            {
                Console.WriteLine($"\t\tDiscount: {nameof(discount.DisountType)}:{discount.DisountType}, " +
                                     $"{nameof(discount.ValidFrom)}:{discount.ValidFrom}, " +
                                     $"{nameof(discount.ValidTo)}:{discount.ValidTo}, " +
                                     $"{nameof(discount.Value)}:{discount.Value.Value}, " +
                                     $"{nameof(discount.Value.Category)}:{discount.Value.Category},");
                Console.WriteLine($"\t\tIs the discount determined as valid based on the Strategy Predicates?: {strategy.Predicates.All(p => p(discount))}\n");
            }
            Console.WriteLine();




            IOrderService<IProduct, IOrder> orderService = new OrderService<IOrder, IProduct>(
                                                                                                order,
                                                                                                targetCurrency,
                                                                                                services.stockService,
                                                                                                services.shipmentService,
                                                                                                services.paymentService,
                                                                                                services.priceCalculator,
                                                                                                services.taxProvider,
                                                                                                services.discountService,
                                                                                                services.currencyConverterFactory
                                                                                                                                    );
            Console.WriteLine($"Price per unit: {order.Product.PricePerUnit.Amount} {order.Product.PricePerUnit.Currency.Value}");

            var pricePerUnitInTargetCurrency = DataSources.TestData.CurrencyConverter.Convert(targetCurrency.Value, order.Product.PricePerUnit).Amount;
            Console.WriteLine($"Price per unit in target currency: {pricePerUnitInTargetCurrency} {targetCurrency.Value}");

            Console.WriteLine($"Tax: ({services.taxProvider.Tax.Name}, {services.taxProvider.Tax.Rate}%)");

            var aggregatedDiscount = services.discountService.AggregatedDiscount(order);
            //To console every field of aggregatedDisount
            Console.WriteLine($"aggregatedDisount : Discount Type: {aggregatedDiscount.DisountType}");
            Console.WriteLine($"aggregatedDisount : Valid From: {aggregatedDiscount.ValidFrom}");
            Console.WriteLine($"aggregatedDisount: Valid To: {aggregatedDiscount.ValidTo}");
            Console.WriteLine($"aggregatedDisount : Value: {aggregatedDiscount.Value}");

            var expectedTotal = order.NumberOfUnits
                * pricePerUnitInTargetCurrency
                * (1 - aggregatedDiscount.Value.Value / 100)
                * (1 + services.taxProvider.Tax.Rate / 100m);

            Console.WriteLine($"Expected total: {order.NumberOfUnits} " +
                $"* {pricePerUnitInTargetCurrency} " +
                $"* {(1 - aggregatedDiscount.Value.Value / 100)} (discount = {aggregatedDiscount.Value.Value}%) " +
                $"* {(1 + services.taxProvider.Tax.Rate / 100m)} (tax = {services.taxProvider.Tax.Rate}%)= {expectedTotal} {targetCurrency.Value}");


            // Act


            IPrice totalPrice = orderService.GetTotal();
            Console.WriteLine($"Actual total price: {totalPrice.Amount} {totalPrice.Currency.Value}");


            // Assert
            Assert.AreEqual(expectedTotal, totalPrice.Amount);

        }
        [TestMethod()]
        public void Test_01_Get_Total_in_PLN()
        {
            IOrder order = DataSources.TestData.Orders.First();
            Test_Get_Total_in_PLN(order);
        }

        [TestMethod()]
        public void Test_02_Get_Total_for_Each_Order_in_PLN()
        {
            foreach (var order in DataSources.TestData.Orders)
            {

                Test_Get_Total_in_PLN(order);
            }
        }

    }
}