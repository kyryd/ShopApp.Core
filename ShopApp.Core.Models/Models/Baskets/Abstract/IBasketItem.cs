using ShopApp.Core.Models.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Models.Baskets.Abstract
{
    public interface IBasketItem
    {
        int ProductId { get; }
        int Quantity { get; }
        DateTime Date { get; }
        IPrice Price { get; }
    }
}
