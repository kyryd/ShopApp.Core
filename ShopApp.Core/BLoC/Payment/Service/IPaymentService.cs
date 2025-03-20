using ShopApp.Core.Enums;
using ShopApp.Core.Models.Payment.Abstract;

namespace ShopApp.Core.BLoC.Payment.Service
{
    public interface IPaymentService
    {
        Task<IPaymentState> Pay(IPayment payment);
        Task<IPaymentState> Cancel(int transacsionId);
        Task<IPaymentState> Refund();
    }
}
