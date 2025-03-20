using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Discounts;

namespace ShopApp.Core.Models.Models.Abstract
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