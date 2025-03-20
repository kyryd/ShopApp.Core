﻿using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Core.Abstract
{
    public interface IDecimalValue
    {
        public decimal Value { get; }
        public ValueCategory Category { get; }
    }
}
