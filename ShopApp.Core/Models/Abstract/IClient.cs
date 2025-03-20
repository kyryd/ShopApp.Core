namespace ShopApp.Core.Models.Abstract
{
    public interface IClient
    {
        string FirstName { get; }
        string LastName { get; }
        IAddress ClientAddress { get; }
        IEnumerable<IAddress> DeliveryAddresses { get; }
        bool IsVip { get;}
    }
}