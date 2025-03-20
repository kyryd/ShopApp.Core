using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models.Discounts.Abstract
{
    public interface IDiscount
    {
        public DiscountType DisountType { get; init; }
        public DateOnly ValidFrom { get; init; }
        public DateOnly ValidTo { get; init; }
        public IDecimalValue Value { get; init; }
    }
}