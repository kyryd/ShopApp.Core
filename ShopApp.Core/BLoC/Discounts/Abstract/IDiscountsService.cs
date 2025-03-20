using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;

namespace ShopApp.Core.BLoC.Discounts.Abstract
{
    public interface IDiscountsService<out D> where D : IDiscount
    {
        D AggregatedDiscount(IOrder order);
        IReadOnlyCollection<D> ApprovedDiscounts(IOrder order);
    }
}