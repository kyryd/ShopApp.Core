namespace ShopApp.Core.Enums
{
    public enum ValueCategory
    {
        absolute,
        percentage
    }

    public static class ValueCategoryExtensions
    {
        public static bool IsAbsolute(this ValueCategory valueCategory) => valueCategory == ValueCategory.absolute;
        public static bool IsPercentage(this ValueCategory valueCategory) => valueCategory == ValueCategory.percentage;
    }
}
