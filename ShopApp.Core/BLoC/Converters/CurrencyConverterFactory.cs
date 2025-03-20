using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Converters
{
    public class CurrencyConverterFactory<CC>(IDictionary<(Currencies from, Currencies to), decimal> exchangeRates) : ICurrencyConverterFactory<CC> where CC : ICurrencyConverter, new()
    {
        IDictionary<(Currencies from, Currencies to), decimal> ExchangeRates { get; } = exchangeRates;

        private static CC? _instance;
        public ICurrencyConverter GetInstance()
        {
            return _instance ??= new CC() { ExchangeRates = ExchangeRates};
        }
        public ICurrencyConverter GetNewInstance()
        {
            return new CC() { ExchangeRates = ExchangeRates };
        }
    }
}
