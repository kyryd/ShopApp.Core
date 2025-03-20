namespace ShopApp.Core.Models.Models.Abstract
{
    public interface IPrice
    {
        decimal Amount { get; set; }
        ICurrency Currency { get; }

        //public abstract IPrice Add(IPrice b);

        //public static IPrice operator +(IPrice a, IPrice b) => a.Add(b);
        //public static decimal operator *(IPrice a, decimal b) => a.Amount * b;
        //public ICurrencyConverter CurrencyConverter { get; }

    }
}
