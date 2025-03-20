using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Core.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Models.Models.Discounts
{
    public sealed record OrderDiscount : Discount
    {
        public OrderDiscount() : base() {
            //throw new NotImplementedException();
        }
        public OrderDiscount(DiscountType DisountType, DateOnly ValidFrom, DateOnly ValidUntill, IDecimalValue Value) :  base(DisountType, ValidFrom, ValidUntill, Value)
        {
            
        }
    }
}
