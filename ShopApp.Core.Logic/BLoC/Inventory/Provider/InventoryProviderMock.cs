using ShopApp.Core.Logic.BLoC.Inventory.Provider.Abstract;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Stock.Abstract;

namespace ShopApp.Core.Logic.BLoC.Inventory.Provider
{
    public class InventoryProviderMock : IInventoryProvider<IProduct>
    {
        public Task<bool> AddToStock(IProduct product, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddToStock(int productId, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<IStock<IProduct>> CheckStatus(IProduct product)
        {
            throw new NotImplementedException();
        }

        public Task<IStock<IProduct>> CheckStatus(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IStock<IProduct>> Deduct(IProduct product, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Release(IProduct product, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Release(int productId, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromStock(IProduct product, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromStock(int productId, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Reserve(IProduct product, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Reserve(int productId, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<IStock<IProduct>> Supply(IProduct product, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
