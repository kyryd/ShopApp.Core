﻿using ShopApp.Core.BLoC.Discounts.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;
using ShopApp.Core.Tests.DataSources;
using System.Linq.Expressions;

namespace ShopApp.Core.Tests.BLoC.Discounts
{
    [TestClass()]
    public class DisountStrategyProviderMockTests
    {
        [TestMethod()]
        public void Test_01_Strategy()
        {
            IDiscountsStrategyProvider provider = MockData.MockStrategyProviderWith4PredicatesAnd2ValueSelectors();

            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Value    :  {provider.Strategy.MaxPercentDiscount.Value.Value}");
            Console.WriteLine($"strategyProvider.Strategy.MaxPercentDiscount.Value.Category :  {provider.Strategy.MaxPercentDiscount.Value.Category}");
            Console.WriteLine($"strategyProvider.Strategy.Predicates.Count :  {provider.Strategy.Predicates.Count}");

            Assert.AreEqual(4, provider.Strategy.Predicates.Count);
            Assert.AreEqual(20m, provider.Strategy.MaxPercentDiscount.Value.Value);
            Assert.IsNotNull(provider.Strategy);
        }


    }


}