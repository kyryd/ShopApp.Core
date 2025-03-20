using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Stock.Abstract;

namespace ShopApp.Core.Logic.BLoC.Inventory.Provider.Abstract
{
    public interface IInventoryProvider<P> where P : IProduct
    {
        Task<bool> Reserve(P product, decimal quantity);
        Task<bool> Reserve(int productId, decimal quantity);

        Task<bool> Release(P product, decimal quantity);
        Task<bool> Release(int productId, decimal quantity);

        Task<bool> RemoveFromStock(P product, decimal quantity);
        Task<bool> RemoveFromStock(int productId, decimal quantity);
        Task<IStock<P>> Deduct(P product, decimal amount);

        Task<bool> AddToStock(P product, decimal quantity);
        Task<bool> AddToStock(int productId, decimal quantity);
        Task<IStock<P>> Supply(P product, decimal amount);


        Task<IStock<P>> CheckStatus(P product);
        Task<IStock<P>> CheckStatus(int productId);

    }
}
