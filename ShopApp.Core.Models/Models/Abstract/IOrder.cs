using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Discounts;

namespace ShopApp.Core.Models.Models.Abstract
{
    public interface IOrder
    {
        IClient Consumer { get; }
        DateTime OrderTime { get; }
        IList<OrderDiscount> Discounts { get; }
        IProduct Product { get; }
        decimal NumberOfUnits { get; }
        DeliveryState DeliveryState { get; set; }
        PaymentState PaymentState { get; set; }
    }
}