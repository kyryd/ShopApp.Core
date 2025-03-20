using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Models.Baskets
{
    public record BasketItem(int ProductId, int Quantity, DateTime Date, IPrice Price) : Entity, IBasketItem;

}
