using ShopApp.Core.Models.Models.Address.Abstract;

namespace ShopApp.Core.Models.Models.Abstract
{
    public interface ISeller
    {
        string Name { get; }
        IAddress SellerAddress { get; }
        IEnumerable<ICategory> Categories { get; }
    }
}
