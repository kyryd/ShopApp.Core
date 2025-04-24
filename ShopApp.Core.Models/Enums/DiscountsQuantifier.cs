namespace ShopApp.Core.Models.Enums
{
    public enum DiscountsQuantifier
    {
        Single = 0,
        Multiple = 1,
    }

    public enum DiscountsPriority
    {
        ByOrder = 0,
        ByDiscountAmount = 1,
        ByTime = 2,
        ByItemsAmount = 3,
    }

    public enum DiscountsLevels
    {
        Product = 0b0001,
        Order = 0b0010,
        Category = 0b0100,
    }

}
