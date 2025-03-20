using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models.Discounts.Abstract
{
    public interface IDiscountsStrategy
    {
        //ISet<DiscountType> Type { get; }
        //DiscountsQuantifier Quantifier { get; }
        //DiscountsPriority Priority { get; }
        //ISet<DiscountsLevels> Levels { get; }

        ISet<Predicate<IDiscount>> Predicates { get; }

        Func<IEnumerable<IDiscount>, DateOnly>? ValidFromSelector { get; }
        Func<IEnumerable<IDiscount>, DateOnly>? ValidToSelector { get; }
        Func<IEnumerable<IDiscount>, IDecimalValue>? ValueSelector { get; }
        Func<IEnumerable<IDiscount>, decimal>? ValueValueSelector { get; }
        Func<IEnumerable<IDiscount>, ValueCategory>? ValueCategorySelector { get; }

        IDiscount MaxAbsoluteDicount { get; }
        IDiscount MaxPercentDiscount { get; }

        //IComparer<DateOnly>? ValidFromComparer { get; }
        //IComparer<DateOnly>? ValidToComparer { get; }
        //IComparer<IDecimalValue>? ValueComparer { get; }
        //IComparer<decimal>? ValueValueComparer { get; }
        //IComparer<ValueCategory>? ValueCategoryComparer { get; }
    }
}
