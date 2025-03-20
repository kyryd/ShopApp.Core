using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Prices.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;
using ShopApp.Core.Models.Models.Taxes.Abstract;
using System.Diagnostics;
using System.IO.Pipelines;

namespace ShopApp.Core.Logic.BLoC.Prices
{
    public class PriceCalculator(CurrencyConverterFactory<CurrencyConverter> converterFactory) : IPriceCaluclator<IPrice>
    {
        protected CurrencyConverterFactory<CurrencyConverter> ConverterFactory { get; } = converterFactory;

        public IPrice Add(IPrice a, IPrice b)
        {
            if (a.Currency.Value == b.Currency.Value)
            {
                return new Price(a.Currency, a.Amount + b.Amount);
            }

            return new Price(a.Currency,  a.Amount + ConverterFactory.GetInstance().Convert(a.Currency.Value, b).Amount);
        }

        public IPrice CalcTotal(IPrice price, decimal amount)
        {
            return new Price(price.Currency, price.Amount * amount);
        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount)
        {
            var totalPriceBeforeDiscount = CalcTotal(price, amount);

            if (discount == null)
            {
                return totalPriceBeforeDiscount;
            }

            if (discount.Value.Category.IsAbsolute())
            {
                return new Price(price.Currency, totalPriceBeforeDiscount.Amount - discount.Value.Value);
            }
            else if (discount.Value.Category.IsPercentage())
            {
                return new Price(price.Currency, totalPriceBeforeDiscount.Amount * (1 - discount.Value.Value / 100m));
            }
            else
            {
                throw new NotImplementedException();
            }


        }

        public IPrice CalcTotal(IPrice price, decimal amount, ICurrency targetCurrency)
        {
            if (targetCurrency.Value == price.Currency.Value)
            {
                return CalcTotal(price, amount);
            }
            var converter = ConverterFactory.GetNewInstance();

            var priceInTargetCurrency = converter.Convert(targetCurrency.Value, price);

            Debug.Assert(priceInTargetCurrency is Price);

            return CalcTotal((Price)priceInTargetCurrency, amount);
        }
        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ICurrency targetCurrency)
        {
            if (targetCurrency.Value == price.Currency.Value)
            {
                return CalcTotal(price, amount, discount);
            }

            var converter = ConverterFactory.GetNewInstance();
            var priceInTargetCurrency = converter.Convert(targetCurrency.Value, price);

            Debug.Assert(priceInTargetCurrency is Price);

            return CalcTotal((Price)priceInTargetCurrency, amount, discount);
        }

        public IPrice CalcTotal(IPrice price, decimal amount, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount);

            var taxRate = tax.Rate;

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100));

        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount, discount);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100));
        }

        public IPrice CalcTotal(IPrice price, decimal amount, ICurrency targetCurrency, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount, targetCurrency);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100));
        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ICurrency targetCurrency, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount, discount, targetCurrency);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100));

        }

    }
}
