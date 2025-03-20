using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Payment.Abstract
{
    public interface IPaymentState
    {
        public IPaymentTransaction? Transaction { get; }
        public PaymentState State { get; }
    }
}
