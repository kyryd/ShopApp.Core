using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;
using ShopApp.Core.Models.Models.Discounts;
using System.Globalization;

namespace ShopApp.Core.Models.Models
{
    public sealed record Order(
                        IClient Consumer,
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
