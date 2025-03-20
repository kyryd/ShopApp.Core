using ShopApp.Core.BLoC.Discounts.Abstract;
using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core;
using ShopApp.Core.Models.Discounts;
using ShopApp.Core.Models.Discounts.Abstract;

namespace ShopApp.Core.BLoC.Discounts
{
    class ValidDiscountsProvider : IDiscountsProvider
    {
        public IList<IDiscount> Discounts { get; }
        public ValidDiscountsProvider(IReadOnlyCollection<IDiscount> discounts)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            Discounts = [.. discounts.Where(d => d.ValidFrom <= now  && now <= d.ValidTo)];
        }

    }
}
