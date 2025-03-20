using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;

namespace ShopApp.Core.Logic.BLoC.Orders.Abstract
{
    public interface IOrderService<P, O> where P : IProduct where O : IOrder
    {
        public O Order { get; init; }
        IPrice GetTotal();
        Task<bool> ReserveInStock();
        Task<PaymentState> ProcessPayment();
        Task<DeliveryState> Deliver();
    }
}
