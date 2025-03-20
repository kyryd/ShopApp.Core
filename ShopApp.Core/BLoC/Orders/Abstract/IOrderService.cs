using ShopApp.Core.BLoC.Converters.Abstract;
using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;

namespace ShopApp.Core.BLoC.Orders.Abstract
{
    public interface IOrderService<P,O> where P : IProduct where O : IOrder
    {
        public O Order { get; init; }
        IPrice GetTotal();
        Task<bool> ReserveInStock();
        Task<PaymentState> ProcessPayment();
        Task<DeliveryState> Deliver();
    }
}
