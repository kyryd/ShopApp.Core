using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Models.Models.Client
{
    public record Client(string FirstName, string LastName, bool IsVip, IAddress ClientAddress, IList<IAddress>? DeliveryAddresses = null) : Entity, IClient;

}
