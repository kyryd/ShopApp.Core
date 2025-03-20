using ShopApp.Core.Models.Models.Address.Abstract;

namespace ShopApp.Core.Models.Models.Client.Abstract
{
    public interface IClient
    {
        string FirstName { get; init; }
        string LastName { get; init; }
        IAddress ClientAddress { get; init; }
        IList<IAddress>? DeliveryAddresses { get; init; }
        bool IsVip { get; init; }
    }
}