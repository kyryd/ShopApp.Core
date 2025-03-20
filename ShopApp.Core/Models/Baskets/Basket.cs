// See https://aka.ms/new-console-template for more information
using ShopApp.Core.Models.Abstract;

public class Basket : IBasket
{
    public IClient Consumer { get; set; }
    public IList<IOrder> Orders { get; set; }

    public int GetDiscount()
    {
        throw new NotImplementedException();
    }

    public int TotalPrice()
    {
        throw new NotImplementedException();
    }

    //public int totalPrice()
    //{
    //    int total = 0;
    //    foreach (var order in Orders)
    //    {
    //        foreach (var product in order.Products)
    //        {
    //            total += product.Price;
    //        }
    //    }
    //    return total;
    //}
    //public int getDiscount()
    //{
    //    if (Consumer.IsVip)
    //    {
    //        return totalPrice() * 20 / 100;
    //    }
    //    else if (totalPrice() > 1000)
    //    {
    //        return totalPrice() * 10 / 100;
    //    }
    //    else ()
    //    {
    //        return 0;
    //    }
    //    return 0;
    //}
}
