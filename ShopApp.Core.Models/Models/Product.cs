using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;
using ShopApp.Core.Models.Models.Discounts;

namespace ShopApp.Core.Models.Models
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
