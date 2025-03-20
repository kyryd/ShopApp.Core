using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models.Discounts.Abstract
{
    public abstract record Discount() : Entity, IDiscount {
      public DiscountType DisountType { get; init; }
      public DateOnly ValidFrom { get; init; }
      public DateOnly ValidTo { get; init; }
      public IDecimalValue Value { get; init; }

        public Discount(DiscountType DisountType, DateOnly ValidFrom, DateOnly ValidUntill, IDecimalValue Value) : this()
        {
            this.DisountType = DisountType;
            this.ValidFrom = ValidFrom;
            this.ValidTo = ValidUntill;
            this.Value = Value;
        }
    }
}
