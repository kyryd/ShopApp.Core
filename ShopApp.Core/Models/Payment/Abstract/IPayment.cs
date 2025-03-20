﻿using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Payment.Abstract
{
    public interface IPayment
    {
        public decimal Amount { get; }
        public Currencies Currency { get;}
    }
}
