namespace ShopApp.Core.Models.Models.Address.Abstract
{
    public interface IAddress
    {
        string Street { get; init; }
        string City { get; init; }
        string State { get; init; }
        string Zip { get; init; }
        string Country { get; init; }
    }
}
