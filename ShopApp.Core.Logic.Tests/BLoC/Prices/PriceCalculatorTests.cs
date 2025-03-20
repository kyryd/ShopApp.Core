using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Prices;
using ShopApp.Core.Logic.Tests.DataSources;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core;
using ShopApp.Core.Models.Models.Discounts;

namespace ShopApp.Core.Logic.Tests.BLoC.Prices
{
    [TestClass()]
    public class PriceCalculatorTests
    {
        private static void AssertPriceAndOutputResults(decimal expected, decimal actual)
        {
            Console.WriteLine($"Expected total: {expected}");
            Console.WriteLine($"Actual total: {actual}");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_01_Calculate_Total()
        {
            // Arrange
            var usd = MockData.MockCurrencyUSD();

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            // Act
            var total = priceCalculator.CalcTotal(new Price(usd, 100m), 2);

            // Assert
            var expectedTotal = 200m;
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);
        }

        [TestMethod()]
        public void Test_02_Calculate_Total_And_Percent_Disount()
        {
            // Arrange
            var usd = MockData.MockCurrencyUSD();

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(25, ValueCategory.percentage));

            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount);

            // Assert
            var expectedTotal = 200m * (1 - 0.25m);
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);

        }

        [TestMethod()]
        public void Test_03_Calculate_Total_And_Absolute_Disount()
        {
            // Arrange
            var usd = MockData.MockCurrencyUSD();

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(1, ValueCategory.absolute));

            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount);

            // Assert
            var expectedTotal = 200m - 1m;
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);

        }

        [TestMethod()]
        public void Test_04_Calculate_Total_And_Percent_Disount_To_Pln()
        {
            // Arrange
            var usd = TestData.CurrenciesList.First(c => c.Value == Currencies.USD);
            Console.WriteLine($"Source Currency: {usd.Value}");

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(25, ValueCategory.percentage));

            //{ (Currencies.USD, Currencies.PLN), 3.8m },
            var pln = TestData.CurrenciesList.First(c => c.Value == Currencies.PLN);
            Console.WriteLine($"Destination Currency: {pln.Value}");
            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount, pln);


            // Assert
            var expectedTotal = 200m * 3.8m * (1 - 0.25m);
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);

        }

        [TestMethod()]
        public void Test_05_Calculate_Total_And_Absolute_Disount_To_PLN()
        {
            // Arrange
            var usd = TestData.CurrenciesList.First(c => c.Value == Currencies.USD);
            Console.WriteLine($"Source Currency: {usd.Value}");

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(1, ValueCategory.absolute));

            //{ (Currencies.USD, Currencies.PLN), 3.8m },
            var pln = TestData.CurrenciesList.First(c => c.Value == Currencies.PLN);
            Console.WriteLine($"Destination Currency: {pln.Value}");
            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount, pln);

            // Assert
            var expectedTotal = 200m * 3.8m - 1m;
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);
        }

        [TestMethod()]
        public void Test_06_Calculate_Total_And_Percent_Disount_With_Tax_To_Pln()
        {
            // Arrange
            var usd = TestData.CurrenciesList.First(c => c.Value == Currencies.USD);
            Console.WriteLine($"Source Currency: {usd.Value}");

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(25, ValueCategory.percentage));

            //{ (Currencies.USD, Currencies.PLN), 3.8m },
            var pln = TestData.CurrenciesList.First(c => c.Value == Currencies.PLN);
            Console.WriteLine($"Destination Currency: {pln.Value}");

            var tax = MockData.Mock20PercentTaxPLN();

            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount, pln, tax);


            // Assert
            var expectedTotal = 200m * 3.8m * (1 - 0.25m) * (1m + 0.20m);//(amount * price * exchangeRate *(1 - percent discount)) * (1 + tax)
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);

        }

        [TestMethod()]
        public void Test_07_Calculate_Total_And_Absolute_Disount_With_Tax_To_PLN()
        {
            // Arrange
            var usd = TestData.CurrenciesList.First(c => c.Value == Currencies.USD);
            Console.WriteLine($"Source Currency: {usd.Value}");

            CurrencyConverterFactory<CurrencyConverter> converterFactory = new CurrencyConverterFactory<CurrencyConverter>(exchangeRates: TestData.ExchangeRates);
            var priceCalculator = new PriceCalculator(converterFactory);

            var price = new Price(usd, 100m);
            var discount = new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(1, ValueCategory.absolute));

            //{ (Currencies.USD, Currencies.PLN), 3.8m },
            var pln = TestData.CurrenciesList.First(c => c.Value == Currencies.PLN);
            Console.WriteLine($"Destination Currency: {pln.Value}");

            var tax = MockData.Mock20PercentTaxPLN();
            // Act
            var total = priceCalculator.CalcTotal(price, 2, discount, pln, tax);

            // Assert
            var expectedTotal = (2 * 100m * 3.8m - 1m) * (1 + 0.20m);//(amount * price * exchangeRate - absolute discount) * (1 + tax)
            Console.WriteLine($"Expected total: {expectedTotal}");
            Console.WriteLine($"Actual total: {total.Amount}");
            Assert.AreEqual(expectedTotal, total.Amount);
        }
        
        [TestMethod()]
        public void Test_08_Add_Prices()
        {
            // Arrange
                Currency fromCurrency = new Currency(TestData.ExchangeRates.First().Key.from);
                Console.WriteLine($"From currency: {fromCurrency.Value}");

                Currency toCurrency = new Currency(TestData.ExchangeRates.First().Key.to);
                Console.WriteLine($"To currency: {toCurrency.Value}");

                var expectedRate = TestData.ExchangeRates.First().Value;
                Console.WriteLine($"Expected rate: {expectedRate}");

                IPrice price = new Price(toCurrency, 10m);
                Console.WriteLine($"Price amount: {price.Amount} {price.Currency.Value}");

                IPrice priceToAdd = new Price(fromCurrency, 10m);
                Console.WriteLine($"Price to add amount: {priceToAdd.Amount} {priceToAdd.Currency.Value}");

                decimal expectedAmount = price.Amount + priceToAdd.Amount * expectedRate;
                Console.WriteLine($"Expected amount: {expectedAmount} {toCurrency.Value}");

                CurrencyConverterFactory<CurrencyConverter> converterFactory = 
                    new CurrencyConverterFactory<CurrencyConverter>(TestData.ExchangeRates);
                
                PriceCalculator priceCalculator = new PriceCalculator(converterFactory);
            //Act: add a price to the price

                IPrice sum = priceCalculator.Add((Price)price, (Price)priceToAdd);
                Console.WriteLine($"Total amount: {sum.Amount} {sum.Currency.Value}");

            //Assert: check if the price is correct
                Assert.AreNotEqual(20m, sum.Amount, "The total amount != 20.");

                Assert.AreEqual(expectedAmount, sum.Amount, $"The expected amount is {expectedAmount}.");

        }

    }
}