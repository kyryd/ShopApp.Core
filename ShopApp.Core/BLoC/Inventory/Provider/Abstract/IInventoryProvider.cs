using ShopApp.Core.Models;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Stock.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Inventory.Provider.Abstract
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
