using ShopApp.Core.Models.Discounts.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Discounts.Abstract
{
    public interface IDiscountsProvider
    {
        IList<IDiscount> Discounts { get; }
    }
}
