using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;

namespace ShopApp.Core.Models.Discounts
{
    public sealed record CategoryDiscount : Discount
    {
        public CategoryDiscount(DiscountType DisountType, DateOnly ValidFrom, DateOnly ValidUntill, IDecimalValue Value) : base(DisountType, ValidFrom, ValidUntill, Value)
        {
        }
    }
}
