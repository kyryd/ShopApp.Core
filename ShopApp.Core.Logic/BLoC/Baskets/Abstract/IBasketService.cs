using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;

namespace ShopApp.Core.Logic.BLoC.Baskets.Abstract
{
    interface IBasketService
    {
        public Task<IBasket> CreateBasket(IClient client);
        public Task<IBasket> RemoveBasket(IClient client);
        //public Task<IBasket> GetBasket(int id);
        //public Task<IBasket> GetBasket(IClient client);

        //public Task<bool> AddItem(IBasket basket, IProduct product, decimal Quantity);
        //public Task<bool> AddItem(IBasket basket, IBasketItem item);
        //public Task<bool> RemoveItem(IBasket basket, IBasketItem item);
        //public Task<bool> AddItem(int basketId, IBasketItem item);
        //public Task<bool> RemoveItem(int basketId, IBasketItem item);

    }
}
