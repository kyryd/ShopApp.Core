using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Enums
{
    public enum PaymentState
    {
        Unpaid = 0,
        InProcess = 1,
        Paid = 2,
        Refunded = 3,
        Cancelled = 4,
        Pending = 5
    }

    public static class PaymentStateExtensions
    {
        public static bool IsPaid(this PaymentState state) => state == PaymentState.Paid;
        public static bool IsRefunded(this PaymentState state) => state == PaymentState.Refunded;
    }
}
