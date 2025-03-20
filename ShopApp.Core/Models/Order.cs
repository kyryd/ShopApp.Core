using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Core.Abstract;
using ShopApp.Core.Models.Discounts;
using System.Globalization;

namespace ShopApp.Core.Models
{
    public sealed record Order(
                        DateTime OrderTime,
                        IProduct Product,
                        IList<OrderDiscount> Discounts,
                        decimal NumberOfUnits, 
                        PaymentState PaymentState = PaymentState.Unpaid,
                        DeliveryState DeliveryState = DeliveryState.NotShipped) : Entity, IOrder
    {
        public PaymentState PaymentState { get; set; } = PaymentState;
        public DeliveryState DeliveryState { get; set; } = DeliveryState;

    };
    
    
}
