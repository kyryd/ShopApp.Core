using ShopApp.Core.Enums;
using ShopApp.Core.Models.Discounts;

namespace ShopApp.Core.Models.Abstract
{
    public interface IOrder
    {
        DateTime OrderTime { get; }
        IList<OrderDiscount> Discounts { get; }
        IProduct Product { get; }
        decimal NumberOfUnits { get; }
        //KeyValuePair<(Currencies, Currencies), decimal> RateAtOrderTime { get; }
        DeliveryState DeliveryState { get; set; }
        PaymentState PaymentState { get; set; }
    }
}