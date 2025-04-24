using ShopApp.Core.Logic.BLoC.Discounts.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;
using ShopApp.Core.Models.Models.Discounts;

namespace ShopApp.Core.Logic.BLoC.Discounts
{
    public class DisountStrategyProviderMock : IDiscountsStrategyProvider
    {
        public IDiscountsStrategy Strategy => new DiscountsStrategyMock();
    }
}
