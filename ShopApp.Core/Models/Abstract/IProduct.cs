using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;
using ShopApp.Core.Models.Discounts;

namespace ShopApp.Core.Models.Abstract
{
    public interface IProduct
    {
        string Name { get; }
        IList<CategoryDiscount> CategoryDiscounts { get; }
        IList<ProductDiscount> ProductDiscounts { get; }
        ICategory Category { get; }
        IPrice PricePerUnit { get; }
        Units Unit { get; }
    }
}