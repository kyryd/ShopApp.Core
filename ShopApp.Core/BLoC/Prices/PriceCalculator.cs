using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.BLoC.Prices.Abstract;
using ShopApp.Core.Enums;
using ShopApp.Core.Models;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;
using ShopApp.Core.Models.Taxes.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Prices
{
    public class PriceCalculator(CurrencyConverterFactory<CurrencyConverter> converterFactory) : IPriceCaluclator<IPrice>
    {
        protected CurrencyConverterFactory<CurrencyConverter> ConverterFactory { get; } = converterFactory;

        public IPrice CalcTotal(IPrice price, decimal amount)
        {
            return new Price(price.Currency, price.Amount * amount, ConverterFactory.GetNewInstance());
        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount)
        {
            var totalPriceBeforeDiscount = CalcTotal(price, amount);
            
            if(discount == null)
            {
                return totalPriceBeforeDiscount;
            }

            if (discount.Value.Category.IsAbsolute())
            {
                return new Price(price.Currency, totalPriceBeforeDiscount.Amount - discount.Value.Value, ConverterFactory.GetNewInstance());
            }
            else if (discount.Value.Category.IsPercentage())
            {
                return new Price(price.Currency, totalPriceBeforeDiscount.Amount * (1- discount.Value.Value / 100m), ConverterFactory.GetNewInstance());
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

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100), ConverterFactory.GetNewInstance());

        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount,discount);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100), ConverterFactory.GetNewInstance());
        }

        public IPrice CalcTotal(IPrice price, decimal amount, ICurrency targetCurrency, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount, targetCurrency);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100), ConverterFactory.GetNewInstance());
        }

        public IPrice CalcTotal(IPrice price, decimal amount, IDiscount discount, ICurrency targetCurrency, ITax tax)
        {
            var beforeTax = CalcTotal(price, amount,discount, targetCurrency);

            return new Price(beforeTax.Currency, beforeTax.Amount * (1 + tax.Rate / 100), ConverterFactory.GetNewInstance());

        }

       


    }
}
