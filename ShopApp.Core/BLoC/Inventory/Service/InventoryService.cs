using ShopApp.Core.BLoC.Inventory.Provider.Abstract;
using ShopApp.Core.BLoC.Inventory.Service.Abstract;
using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;

namespace ShopApp.Core.BLoC.Inventory.Service
{
    public class InventoryService(IInventoryProvider<IProduct> stockProvider) : IInventoryService<IOrder, IProduct>
    {
        //public async Task<bool> Release(IOrder order)
        //{
        //   return await stockProvider.Release(order.Product, order.NumberOfUnits);
        //}

        //public async Task<bool> Reserve(IOrder order)
        //{
        //    return await stockProvider.Reserve(order.Product, order.NumberOfUnits);
        //}

        //public async Task<bool> Deduct(IOrder order)
        //{
        //    return await stockProvider.Reserve(order.Product, order.NumberOfUnits);
        //}
        public async Task<StockState> Deduct(IOrder order)
        {
            return await stockProvider.Reserve(order.Product, order.NumberOfUnits) ? StockState.Deducted : StockState.OutOfStock;
        }

        public async Task<StockState> Release(IOrder order)
        {
            return await stockProvider.Release(order.Product, order.NumberOfUnits) ? StockState.Released : StockState.OutOfStock;
        }

        public async Task<StockState> Reserve(IOrder order)
        {
            return await stockProvider.Reserve(order.Product, order.NumberOfUnits) ? StockState.Reserved : StockState.OutOfStock;
        }
    }
}
