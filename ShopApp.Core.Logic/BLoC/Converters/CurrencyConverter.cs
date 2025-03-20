using ShopApp.Core.Logic.BLoC.Converters.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using System.Diagnostics;

namespace ShopApp.Core.Logic.BLoC.Converters
{
    public sealed class CurrencyConverter() : ICurrencyConverter
    {
        public CurrencyConverter(IDictionary<(Currencies from, Currencies to), decimal> exchangeRates) : this()
        {
            ExchangeRates = exchangeRates;
        }

        public IDictionary<(Currencies from, Currencies to), decimal> ExchangeRates { get; init; }

        public IPrice Convert(Currencies target, IPrice source)
        {
            var rate = GetRate(target, source.Currency) ?? throw new Exception("No rate found for this currency pair");

            return new Price(Currency: new Currency(target),
                              Amount: source.Amount * rate);
        }

        public decimal Convert(Currencies target, Currencies source, decimal sourceValue)
        {
            return sourceValue * (GetRate(target, new Currency(source)) ?? throw new Exception("No rate found for this currency pair"));
        }

        public decimal? GetRate(Currencies target, ICurrency source)
        {
            bool res = ExchangeRates.TryGetValue((source.Value, target), out decimal rateOk);
            Debug.Assert(res, "No rate found for this currency pair");
            return res ? rateOk : null;
        }
    }
}
