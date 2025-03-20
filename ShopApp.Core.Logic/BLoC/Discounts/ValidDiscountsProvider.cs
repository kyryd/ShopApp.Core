using ShopApp.Core.Logic.BLoC.Discounts.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Logic.BLoC.Discounts
{
    class ValidDiscountsProvider : IDiscountsProvider
    {
        public IList<IDiscount> Discounts { get; }
        public ValidDiscountsProvider(IReadOnlyCollection<IDiscount> discounts)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            Discounts = [.. discounts.Where(d => d.ValidFrom <= now && now <= d.ValidTo)];
        }

    }
}
