using ShopApp.Core.Logic.BLoC.Converters.Abstract;
using ShopApp.Core.Models.Enums;

namespace ShopApp.Core.Logic.BLoC.Converters
{
    public class CurrencyConverterFactory<CC>(IDictionary<(Currencies from, Currencies to), decimal> exchangeRates) : ICurrencyConverterFactory<CC> where CC : ICurrencyConverter, new()
    {
        IDictionary<(Currencies from, Currencies to), decimal> ExchangeRates { get; } = exchangeRates;

        private static CC? _instance;
        public ICurrencyConverter GetInstance()
        {
            return _instance ??= new CC() { ExchangeRates = ExchangeRates };
        }
        public ICurrencyConverter GetNewInstance()
        {
            return new CC() { ExchangeRates = ExchangeRates };
        }
    }
}
