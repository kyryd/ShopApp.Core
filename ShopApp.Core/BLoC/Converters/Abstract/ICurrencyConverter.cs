using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;

namespace ShopApp.Core.BLoC.Converters.Abstract
{
    public interface ICurrencyConverter
    {
        IDictionary<(Currencies from, Currencies to), decimal> ExchangeRates { get; init; }
        
        decimal? GetRate(Currencies target, ICurrency source);

        IPrice Convert(Currencies target, IPrice source);
        decimal Convert(Currencies target, Currencies source, decimal sourceValue);
    }
}
