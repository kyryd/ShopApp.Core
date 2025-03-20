using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Payment.Abstract
{
    interface IPaymentCard
    {
        public string CardNumber { get; }
        public string CardHolder { get; }
        public string ExpiryDate { get; }
        public string CVC { get; }
    }
}
