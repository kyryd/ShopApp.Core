using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Enums
{
    public enum Currencies
    {
        PLN,
        EUR,
        UAH,
        USD
    }

    public static class CurrencySymbolsExtensions
    {
        public static string GetSymbol(this Currencies currency)
        {
            return currency switch
            {
                Currencies.PLN => "zł",
                Currencies.EUR => "€",
                Currencies.UAH => "₴",
                Currencies.USD => "$",
                _ => throw new NotImplementedException()
            };
        }
    }
}
