namespace ShopApp.Core.Models.Abstract
{
    public interface ISeller
    {
        string Name { get; }
        IAddress SellerAddress { get; }
        IEnumerable<ICategory> Categories { get; }
    }
}
