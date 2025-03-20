// See https://aka.ms/new-console-template for more information
using ShopApp.Core.Models.Models.Client.Abstract;

namespace ShopApp.Core.Models.Models.Baskets.Abstract
{
    public interface IBasket
    {
        IClient Consumer { get; }
        
        IList<IBasketItem> Items { get; }
    }
}