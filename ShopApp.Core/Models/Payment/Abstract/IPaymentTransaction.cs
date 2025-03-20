using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Payment.Abstract
{
    public interface IPaymentTransaction
    {
        public string TransactionId { get;}
        public string PaymentMethod { get; }
        public decimal Amount { get; }
        public string? State { get; }
        public DateTime TransactionDate { get;}        
    }
}
