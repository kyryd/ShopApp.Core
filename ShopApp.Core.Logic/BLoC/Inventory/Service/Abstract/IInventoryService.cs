using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;

namespace ShopApp.Core.Logic.BLoC.Inventory.Service.Abstract
{
    public interface IInventoryService<O, P> where P : IProduct where O : IOrder
    {
        Task<StockState> Reserve(O order);
        Task<StockState> Release(O order);
        Task<StockState> Deduct(O order);
    }
}
