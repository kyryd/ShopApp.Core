namespace ShopApp.Core.Models.Core.Abstract
{
    public abstract record Entity(int? Id = null) : IEntity;
}
