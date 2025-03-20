using ShopApp.Core.Models.Discounts.Abstract;

namespace ShopApp.Core.BLoC.Discounts.Abstract
{
    public interface IDiscountsStrategyProvider
    {
        IDiscountsStrategy Strategy{ get; }
    }
}
