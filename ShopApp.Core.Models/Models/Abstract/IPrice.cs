namespace ShopApp.Core.Models.Models.Abstract
{
    public interface IPrice
    {
        decimal Amount { get; set; }
        ICurrency Currency { get; }
    }
}
