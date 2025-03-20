using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Stock.Abstract;

namespace ShopApp.Core.BLoC.Inventory.Service.Abstract
{
    public interface IInventoryService<O,P> where P : IProduct where O : IOrder
    {
        Task<StockState> Reserve(O order);
        Task<StockState> Release(O order);
        Task<StockState> Deduct(O order);
    }
}
