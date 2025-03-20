// See https://aka.ms/new-console-template for more information
using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.BLoC.Discounts.Abstract;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Discounts;

namespace ShopApp.Core.BLoC.Discounts
{
    public sealed class OrderDiscountService : DiscountService<OrderDiscount>
    {
        public OrderDiscountService(
                                IDiscountsStrategyProvider strategyProvider,
                                ICurrency currency,
                                ICurrencyConverterFactory<CurrencyConverter> converterFactory
                                                                                                ) : base(strategyProvider, currency, converterFactory)

        {

        }

        public override IReadOnlyCollection<OrderDiscount> ApprovedDiscounts(IOrder order)
        {
            return [.. SelectDiscountByStrategy(StrategyProvider.Strategy, order.Discounts)];
        }

        public override OrderDiscount AggregatedDiscount(IOrder order)
        {
            return AggregateDisounts([.. order.Discounts]);
        }
    }
}