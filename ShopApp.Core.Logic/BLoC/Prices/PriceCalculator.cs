using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Converters.Abstract;
using ShopApp.Core.Logic.BLoC.Prices.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;
using ShopApp.Core.Models.Models.Taxes.Abstract;
using System.Diagnostics;

namespace ShopApp.Core.Logic.BLoC.Prices
{
    public class PriceCalculator(CurrencyConverterFactory<CurrencyConverter> converterFactory) : IPriceCaluclator<IPrice>
    {
        protected CurrencyConverterFactory<CurrencyConverter> ConverterFactory { get; } = converterFactory;

        private static readonly Func<decimal, decimal, decimal> TaxFormula = static (Decimal amount, Decimal taxRate) => amount * (1 + (taxRate / 100));
        private static readonly Func<IPrice, ITax, decimal> TaxFormula2 = static (price, tax) => TaxFormula(price.Amount, tax.Rate);

        private static readonly Func<decimal, decimal, decimal> AbsoluteDicscountFormula = static (Decimal amount, Decimal discount) => amount - discount;
        private static readonly Func<IPrice, IDiscount, decimal> AbsoluteDiscountFormula2 = static (IPrice price, IDiscount discount) => AbsoluteDicscountFormula(price.Amount, discount.Value.Value);

        private static readonly Func<decimal, decimal, decimal> PercentageDiscountFormula = static (Decimal amount, Decimal discount) => amount * (1 - discount / 100m);
        private static readonly Func<IPrice, IDiscount, decimal> PercentageDiscountFormula2 = static (IPrice price, IDiscount discount) => PercentageDiscountFormula(price.Amount, discount.Value.Value);

#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        private static IPrice ApplyTax(IPrice priceBeforeTax, ITax tax) => new Price(priceBeforeTax.Currency, TaxFormula2(priceBeforeTax, tax));
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

        public IPrice Add(IPrice a, IPrice b)
        {
            if (a.Currency.Value == b.Currency.Value)
            {
                return new Price(a.Currency, a.Amount + b.Amount);
            }

            return new Price(a.Currency, a.Amount + ConverterFactory.GetInstance().Convert(a.Currency.Value, b).Amount);
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
                return new Price(price.Currency, AbsoluteDiscountFormula2(totalPriceBeforeDiscount, discount));
            }
            else if (discount.Value.Category.IsPercentage())
            {
                return new Price(price.Currency, PercentageDiscountFormula2(totalPriceBeforeDiscount, discount));
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
            ICurrencyConverter converter = ConverterFactory.GetNewInstance();
            IPrice priceInTargetCurrency = converter.Convert(targetCurrency.Value, price);

            Debug.Assert(priceInTargetCurrency is Price);

            return CalcTotal(priceInTargetCurrency, amount);
        }
        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ICurrency targetCurrency)
        {
            if (targetCurrency.Value == price.Currency.Value)
            {
                return CalcTotal(price, amount, discount);
            }

            ICurrencyConverter converter = ConverterFactory.GetNewInstance();
            IPrice priceInTargetCurrency = converter.Convert(targetCurrency.Value, price);

            Debug.Assert(priceInTargetCurrency is Price);

            return CalcTotal(priceInTargetCurrency, amount, discount);
        }

        public IPrice CalcTotal(IPrice price, decimal amount, ITax tax)
        {
            IPrice priceBeforeTax = CalcTotal(price, amount);

            return ApplyTax(priceBeforeTax, tax);

        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ITax tax)
        {
            IPrice priceBeforeTax = CalcTotal(price, amount, discount);

            return ApplyTax(priceBeforeTax, tax);
        }

        public IPrice CalcTotal(IPrice price, decimal amount, ICurrency targetCurrency, ITax tax)
        {
            IPrice priceBeforeTax = CalcTotal(price, amount, targetCurrency);

            return ApplyTax(priceBeforeTax, tax);
        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ICurrency targetCurrency, ITax tax)
        {
            IPrice priceBeforeTax = CalcTotal(price, amount, discount, targetCurrency);

            return ApplyTax(priceBeforeTax, tax);

        }

    }
}
