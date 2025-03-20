using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models
{
    public sealed record Price(ICurrency Currency, decimal Amount, ICurrencyConverter CurrencyConverter) : Entity, IPrice
    {

        public decimal Amount { get; set; } = Amount;


        public IPrice Add(IPrice b)
        {
            if (Currency.Value != b.Currency.Value)
            {
                return new Price(Currency, Amount + CurrencyConverter.Convert(Currency.Value, b).Amount, CurrencyConverter);
            }

            return new Price(Currency, Amount + b.Amount, CurrencyConverter);
        }

        //public static Price operator +(Price a, Price b) => (Price)a.Add(b);
        //public static Price operator +(Price a, IPrice b) => (Price)a.Add(b);
        //public static decimal operator *(Price a, decimal b) => a.Amount * b;

        //public static IPrice Muliply(IPrice a, decimal b) =>
        //        new Price(Currency: a.Currency,
        //                Amount: a * b,
        //                CurrencyConverter: a.CurrencyConverter
        //              );

    }
}
