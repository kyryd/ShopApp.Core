﻿using ShopApp.Core.BLoC.Discounts.Abstract;
using ShopApp.Core.Models.Discounts;
using ShopApp.Core.Models.Discounts.Abstract;

namespace ShopApp.Core.BLoC.Discounts
{
    public class DisountStrategyProviderMock : IDiscountsStrategyProvider
    {
        //public IDiscountsStrategy Strategy => throw new NotImplementedException();

        public IDiscountsStrategy Strategy => new DiscountsStrategyMock();

        //    strategy switch
        //{
        //    DiscountsQuantifier.ByMaxAbsoluteAmount => Discounts.Where(d => d.Value.Category.IsAbsolute()).OrderBy(d => d.Value).Last(),
        //    DiscountsQuantifier.ByMaxPercent => Discounts.Where(d => d.Value.Category.IsPercentage()).OrderBy(d => d.Value).Last(),
        //    DiscountsQuantifier.ByTimeOnlyPercent => Discounts.Where(d => d.DisountType.IsActualTimeDisount()).OrderBy(d => d.Value).Last(),
        //    DiscountsQuantifier.ByOrder => Discounts.OrderBy(d => d.DisountType).Last(),
        //    _ => throw new NotImplementedException()
        //};
    }
}
