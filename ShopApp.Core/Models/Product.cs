using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Core.Abstract;
using ShopApp.Core.Models.Discounts;

namespace ShopApp.Core.Models
{
    public sealed record Product(
                                  string Name,
                                  IPrice PricePerUnit,
                                  ICategory Category,
                                  IList<CategoryDiscount> CategoryDiscounts,
                                  IList<ProductDiscount> ProductDiscounts,
                                  Units Unit
                                                                            ) : Entity, IProduct;

}
