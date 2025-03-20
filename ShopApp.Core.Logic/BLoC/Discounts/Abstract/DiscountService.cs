// See https://aka.ms/new-console-template for more information
using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Converters.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core;
using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Logic.BLoC.Discounts.Abstract
{
    public abstract class DiscountService<D>(
                                                IDiscountsStrategyProvider strategyProvider,
                                                ICurrency currency,
                                                ICurrencyConverterFactory<CurrencyConverter> converterFactory
                                                                                                ) : IDiscountsService<D> where D : IDiscount, new()
    {

        public ICurrency Currency { get; } = currency;
        public IDiscountsStrategyProvider StrategyProvider { get; } = strategyProvider;
        protected ICurrencyConverterFactory<CurrencyConverter> ConverterFactory { get; } = converterFactory;

        private static IEnumerable<D>? OrderFilteredDiscounts<TSource, TSelector>(Func<TSource, TSelector>? selector, IEnumerable<D> discounts) //where TSource : IEnumerable<D>
        {
            IEnumerable<D>? ordered = null;
            if (selector != null)
            {
                ordered = discounts.OrderByDescending(d => selector.Invoke((TSource)discounts));
            }
            return ordered;
        }
        protected static IEnumerable<D> SelectDiscountByStrategy(IDiscountsStrategy strategy, IEnumerable<D> discounts)
        {
            List<D> filtered = [.. discounts.Where(d => strategy.Predicates.All(p => p(d)))];


            IEnumerable<D>? ordered = OrderFilteredDiscounts(strategy.ValidFromSelector, filtered);

            ordered = OrderFilteredDiscounts(strategy.ValidToSelector, ordered ?? filtered);
            ordered = OrderFilteredDiscounts(strategy.ValueCategorySelector, ordered ?? filtered);
            ordered = OrderFilteredDiscounts(strategy.ValueSelector, ordered ?? filtered);
            ordered = OrderFilteredDiscounts(strategy.ValueValueSelector, ordered ?? filtered);

            return ordered ?? filtered;
        }

        protected static D SelectTopDiscountByStrategy(IDiscountsStrategy strategy, IEnumerable<D> discounts)
        {
            return SelectDiscountByStrategy(strategy, discounts).First();
        }

        protected D AggregateDisounts(IReadOnlyCollection<D> discounts)
        {
            var selectedDisounts = SelectDiscountByStrategy(StrategyProvider.Strategy, discounts);

            decimal totalDiscount = 0m;

            foreach (D discount in selectedDisounts)
            {
                totalDiscount += discount.Value.Value;
            }

            var discountLimit = discounts.First().Value.Category.IsPercentage()
                    ? StrategyProvider.Strategy.MaxPercentDiscount?.Value.Value
                    : StrategyProvider.Strategy.MaxAbsoluteDicount?.Value.Value;

            if (discountLimit < totalDiscount)
            {
#if DEBUG
                Console.WriteLine($"Discount limit reached: {discountLimit}");
#endif
            }
            var disountValue = discountLimit != null && discountLimit < totalDiscount ? discountLimit ?? totalDiscount : totalDiscount;

            return new D()
            {
                DisountType = DiscountType.Aggregated,
                ValidFrom = DateOnly.MinValue,
                ValidTo = DateOnly.MaxValue,
                Value = new DecimalValue(disountValue, discounts.First().Value.Category)
            };
        }

        public abstract D AggregatedDiscount(IOrder order);


        public abstract IReadOnlyCollection<D> ApprovedDiscounts(IOrder order);
    }
}