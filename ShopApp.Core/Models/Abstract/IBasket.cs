// See https://aka.ms/new-console-template for more information
namespace ShopApp.Core.Models.Abstract
{
    public interface IBasket
    {
        int TotalPrice();
        int GetDiscount();
        IClient Consumer { get; }
        IList<IOrder> Orders { get; }
    }
}