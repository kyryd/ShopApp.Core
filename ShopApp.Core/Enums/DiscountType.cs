namespace ShopApp.Core.Enums
{
   
    public enum DiscountType
    {
        ForVip,
        OnTotalPrice,
        OnTripleProduct,
        OnDoubleProduct,
        WhenHappyHour,
        Aggregated
    }


    public static class DiscountTypeExtension
    {
        public static bool IsActualTimeDisount(this DiscountType discountType)
        {
            switch (discountType)
            {
                case DiscountType.ForVip:
                    return false;
                case DiscountType.OnTotalPrice:
                    return false;
                case DiscountType.OnTripleProduct:
                    return false;
                case DiscountType.OnDoubleProduct:
                    return false;
                case DiscountType.WhenHappyHour:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }


        public static bool IsVip(this DiscountType discountType) => discountType == DiscountType.ForVip;
    }
}