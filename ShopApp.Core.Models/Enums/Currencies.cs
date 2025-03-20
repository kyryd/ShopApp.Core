namespace ShopApp.Core.Models.Enums
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
