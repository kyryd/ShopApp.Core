using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Enums
{
    public enum DiscountsQuantifier
    {
        Single = 0,
        Multiple = 1,
        //ByOrder = 0,
        //ByMaxAbsoluteAmount = 1,
        //ByMaxPercent = 2,
        //ByTimeOnlyPercent = 3,
        //ByTimeOnlyAbsolute = 4,
        //NoDiscount = 0,
        //DiscountByRate = 1,
        //DiscountByAmount = 2,
        //DiscountByRateAndAmount = 3,
        //TimeDiscount = 4,
    }

    public enum DiscountsPriority
    {
        ByOrder = 0,
        ByDiscountAmount = 1,
        ByTime = 2,
        ByItemsAmount = 3,
    }

    public enum DiscountsLevels
    {
        Product  = 0b0001,
        Order    = 0b0010,
        Category = 0b0100,
    }

    //public enum DiscountsStrategy
    //{
    //    //NoDiscount = 0,
    //    //DiscountByRate = 1,
    //    //DiscountByAmount = 2,
    //    //DiscountByRateAndAmount = 3,
    //    //TimeDiscount = 4,
    //}

    //public enum DiscountsPriority
    //{
    //    Order = 0,
    //    DicountAmount = 1,
    //    TimeSlot = 2,
    //    ItemsAmount = 3,
    //}

    //public enum DiscountsLevels
    //{
    //    Product = 0b0001,
    //    Order = 0b0010,
    //    Category = 0b0100,
    //}
}
