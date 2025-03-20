using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.Models;
using ShopApp.Core.Models.Abstract;

namespace ShopApp.Core.Tests.Models
{
    [TestClass()]
    public class PriceTests
    {
        [TestMethod()]
        public void Test_01_EUR_add_USD()
        {
            // Arrange
                var converter = new CurrencyConverter(DataSources.TestData.ExchangeRates);

            Currency fromCurrency = new Currency(DataSources.TestData.ExchangeRates.First().Key.from);
                Console.WriteLine($"From currency: {fromCurrency.Value}");

            Currency toCurrency = new Currency(DataSources.TestData.ExchangeRates.First().Key.to);
                Console.WriteLine($"To currency: {toCurrency.Value}");

                var expectedRate = DataSources.TestData.ExchangeRates.First().Value;
                Console.WriteLine($"Expected rate: {expectedRate}");

                IPrice price = new Price(toCurrency, 10m, converter);
                Console.WriteLine($"Price amount: {price.Amount} {price.Currency.Value}");

                IPrice priceToAdd = new Price(fromCurrency, 10m, converter);
                Console.WriteLine($"Price to add amount: {priceToAdd.Amount} {priceToAdd.Currency.Value}");

                decimal expectedAmount = price.Amount + priceToAdd.Amount * expectedRate;
                Console.WriteLine($"Expected amount: {expectedAmount} {toCurrency.Value}");

            //Act: add a price to the price
                IPrice total = price + priceToAdd;
                Console.WriteLine($"Total amount: {total.Amount} {total.Currency.Value}");

            //Assert: check if the price is correct
                Assert.AreNotEqual(20m, total.Amount, "The total amount != 20.");

                Assert.AreEqual(expectedAmount, total.Amount, $"The expected amount is {expectedAmount}.");

        }
    }
}