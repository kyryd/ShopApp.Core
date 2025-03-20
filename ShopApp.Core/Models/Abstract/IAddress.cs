namespace ShopApp.Core.Models.Abstract
{
    public interface IAddress
    {
        string Street { get; }
        string City { get; }
        string State { get; }
        string Zip { get; }
    }
}
