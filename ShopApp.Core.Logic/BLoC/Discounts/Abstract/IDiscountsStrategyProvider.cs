using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Logic.BLoC.Discounts.Abstract
{
    public interface IDiscountsStrategyProvider
    {
        IDiscountsStrategy Strategy { get; }
    }
}
