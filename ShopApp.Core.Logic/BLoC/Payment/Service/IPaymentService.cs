using ShopApp.Core.Models.Models.Payment.Abstract;

namespace ShopApp.Core.Logic.BLoC.Payment.Service
{
    public interface IPaymentService
    {
        Task<IPaymentState> Pay(IPayment payment);
        Task<IPaymentState> Cancel(int transacsionId);
        Task<IPaymentState> Refund();
    }
}
