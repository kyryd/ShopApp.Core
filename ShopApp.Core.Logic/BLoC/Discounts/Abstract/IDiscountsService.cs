using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Logic.BLoC.Discounts.Abstract
{
    public interface IDiscountsService<out D> where D : IDiscount
    {
        D AggregatedDiscount(IOrder order);
        IReadOnlyCollection<D> ApprovedDiscounts(IOrder order);
    }
}