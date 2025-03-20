using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Models.Models.Discounts.Abstract
{
    public interface IDiscountsStrategy
    {
        ISet<Predicate<IDiscount>> Predicates { get; }

        IDiscount MaxAbsoluteDicount { get; }
        IDiscount MaxPercentDiscount { get; }

    }
}
