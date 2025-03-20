using ShopApp.Core.BLoC.Payment.Service;
using ShopApp.Core.Models.Payment.Abstract;

namespace ShopApp.Core.BLoC.Payment
{
    public class PaymentServiceMock : IPaymentService
    {
        public Task<IPaymentState> Cancel(int transacsionId)
        {
            throw new NotImplementedException();
        }

        public Task<IPaymentState> Pay(IPayment payment)
        {
            throw new NotImplementedException();
        }

        public Task<IPaymentState> Refund()
        {
            throw new NotImplementedException();
        }
    }
}
