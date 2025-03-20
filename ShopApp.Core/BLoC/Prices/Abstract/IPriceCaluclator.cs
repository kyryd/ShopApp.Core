using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;
using ShopApp.Core.Models.Taxes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Prices.Abstract
{
    public interface IPriceCaluclator<P> where P: IPrice
    {
        P CalcTotal(P price, decimal amount);
        P CalcTotal(P price, decimal amount, IDiscount discount);
        P CalcTotal(P price, decimal amount, ICurrency targetCurrency);
        P CalcTotal(P price, decimal amount, IDiscount discount, ICurrency targetCurrency);


        P CalcTotal(P price, decimal amount, ITax tax);
        P CalcTotal(P price, decimal amount, IDiscount discount, ITax tax);
        P CalcTotal(P price, decimal amount, ICurrency targetCurrency, ITax tax);
        P CalcTotal(P price, decimal amount, IDiscount discount, ICurrency targetCurrency, ITax tax);
    }
}
