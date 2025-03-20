using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.Enums;
using ShopApp.Core.Models;

namespace ShopApp.Core.Tests.BLoC
{
    [TestClass()]
    public class CurrencyConverterTests
    {
        //Create test methods for CurrencyConverter.GetRate using Data.ExchangeRates
        [TestMethod()]
        public void Test_01_Get_Rate_USD_to_EUR()
        {
            // Arrange
            var converter = new CurrencyConverter(DataSources.TestData.ExchangeRates);

            Currency fromCurrency = new Currency(DataSources.TestData.ExchangeRates.First().Key.from);
            Console.WriteLine($"From currency: {fromCurrency.Value}");

            Currencies toCurrency = DataSources.TestData.ExchangeRates.First().Key.to;
            Console.WriteLine($"To currency: {toCurrency}");

            var expectedRate = DataSources.TestData.ExchangeRates.First().Value;
            Console.WriteLine($"Expected rate: {expectedRate}");

            // Act
            var actualRate = converter.GetRate(toCurrency, fromCurrency);
            Console.WriteLine($"Actual rate: {actualRate}");

            // Assert
            Assert.IsNotNull(actualRate, "The exchange rate should not be null.");
            Assert.AreEqual(expectedRate, actualRate, "The exchange rate should match the expected value.");
        }

        [TestMethod()]
        public void Test_02_Convert_USD_to_EUR()
        {
            // Arrange
            var converter = new CurrencyConverter(DataSources.TestData.ExchangeRates);

            Currency fromCurrency = new Currency(DataSources.TestData.ExchangeRates.First().Key.from);
            Console.WriteLine($"From currency: {fromCurrency.Value}");

            Currencies toCurrency = DataSources.TestData.ExchangeRates.First().Key.to;
            Console.WriteLine($"To currency: {toCurrency}");

            var expectedRate = DataSources.TestData.ExchangeRates.First().Value;
            Console.WriteLine($"Expected rate: {expectedRate}");

            // Act
            var actualRate = converter.GetRate(toCurrency, fromCurrency);
            Console.WriteLine($"Actual rate: {actualRate}");

            var converted = converter.Convert(toCurrency, fromCurrency.Value, 1);
            Console.WriteLine($"Converted: {converted}");

            // Assert
            Assert.IsNotNull(converted, "The converted value should not be null.");
            Assert.AreEqual(expectedRate, converted, "The exchange rate should match the expected value.");
        }
    }
}