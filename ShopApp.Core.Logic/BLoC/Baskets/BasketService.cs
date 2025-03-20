using ShopApp.Core.Logic.BLoC.Baskets.Abstract;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;

namespace ShopApp.Core.Logic.BLoC.Baskets
{
    public class BasketService : IBasketService
    {
        public Task<IBasket> CreateBasket(IClient client)
        {
            throw new NotImplementedException();
        }

        public Task<IBasket> RemoveBasket(IClient client)
        {
            throw new NotImplementedException();
        }
    }
}
