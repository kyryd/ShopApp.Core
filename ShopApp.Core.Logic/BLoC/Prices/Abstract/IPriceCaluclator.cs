using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;
using ShopApp.Core.Models.Models.Taxes.Abstract;

namespace ShopApp.Core.Logic.BLoC.Prices.Abstract
{
    public interface IPriceCaluclator<P> where P : IPrice
    {
        P CalcTotal(P price, decimal amount);
        P CalcTotal(P price, decimal amount, IDiscount discount);
        P CalcTotal(P price, decimal amount, ICurrency targetCurrency);
        P CalcTotal(P price, decimal amount, IDiscount discount, ICurrency targetCurrency);


        P CalcTotal(P price, decimal amount, ITax tax);
        P CalcTotal(P price, decimal amount, IDiscount discount, ITax tax);
        P CalcTotal(P price, decimal amount, ICurrency targetCurrency, ITax tax);
        P CalcTotal(P price, decimal amount, IDiscount discount, ICurrency targetCurrency, ITax tax);

        P Add(P a, P b);
    }
}
