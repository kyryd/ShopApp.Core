using Moq;
using ShopApp.Core.Logic.BLoC.Discounts.Abstract;
using ShopApp.Core.Logic.BLoC.Taxes.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core;
using ShopApp.Core.Models.Models.Discounts;
using ShopApp.Core.Models.Models.Discounts.Abstract;
using ShopApp.Core.Models.Models.Taxes.Abstract;

namespace ShopApp.Core.Logic.Tests.DataSources
{
    public static class MockData
    {
        public static ICurrency MockCurrencyUSD()
        {

            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.USD);
            return mockCurrency.Object;
        }
        public static ICurrency MockCurrencyPLN()
        {

            var mockCurrency = new Mock<ICurrency>();
            mockCurrency.Setup(c => c.Value).Returns(Currencies.USD);
            return mockCurrency.Object;
        }

        public static ITax Mock20PercentTaxPLN()
        {

            var mockTax = new Mock<ITax>();
            //mockTax.Setup(c => c.Currency).Returns(()=>MockCurrencyPLN());
            mockTax.Setup(c => c.Rate).Returns(20m);
            mockTax.Setup(c => c.Name).Returns("20 Percent Tax");
            return mockTax.Object;
        }

        public static ITaxProvider MockTaxProvider20PercentTaxPLN()
        {
            var mockTaxProvider = new Mock<ITaxProvider>();
            mockTaxProvider.Setup(c => c.Tax).Returns(() => Mock20PercentTaxPLN());
            return mockTaxProvider.Object;
        }

        public static ISet<Predicate<IDiscount>> MockPredicates()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            ISet<Predicate<IDiscount>> predicates = new HashSet<Predicate<IDiscount>>([
                (d) =>  d.ValidFrom <= now,
                (d) =>  now <= d.ValidTo,

                (d) =>  d.Value.Category.IsPercentage(),

                (d) => d.DisountType == DiscountType.ForVip || d.DisountType == DiscountType.WhenHappyHour
                ]);
            return predicates;
        }

        public static IDiscountsStrategyProvider MockStrategyProviderWith4PredicatesAnd2ValueSelectors()
        {
            var discountStrategy = new Mock<IDiscountsStrategy>();
            discountStrategy.Setup(ds => ds.Predicates).Returns(MockPredicates());

            discountStrategy.Setup(ds => ds.MaxPercentDiscount).Returns(new OrderDiscount(
                DiscountType.Aggregated,
                ValidFrom: DateOnly.MinValue,
                ValidUntill: DateOnly.MaxValue,
                Value: new DecimalValue(20m, ValueCategory.percentage)));


            var strategyProvider = new Mock<IDiscountsStrategyProvider>();
            strategyProvider.Setup(sp => sp.Strategy).Returns(discountStrategy.Object);

            return strategyProvider.Object;
        }

    }
}
