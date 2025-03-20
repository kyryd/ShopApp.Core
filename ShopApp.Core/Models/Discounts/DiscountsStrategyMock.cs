using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;
using ShopApp.Core.Models.Discounts.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Discounts
{
    class DiscountsStrategyMock : IDiscountsStrategy
    {
        public ISet<Predicate<IDiscount>> Predicates => throw new NotImplementedException();

        public Func<IEnumerable<IDiscount>, DateOnly>? ValidFromSelector => throw new NotImplementedException();

        public Func<IEnumerable<IDiscount>, DateOnly>? ValidToSelector => throw new NotImplementedException();

        public Func<IEnumerable<IDiscount>, IDecimalValue>? ValueSelector => throw new NotImplementedException();

        public Func<IEnumerable<IDiscount>, decimal>? ValueValueSelector => throw new NotImplementedException();

        public Func<IEnumerable<IDiscount>, ValueCategory>? ValueCategorySelector => throw new NotImplementedException();

        public IDiscount MaxAbsoluteDicount => throw new NotImplementedException();

        public IDiscount MaxPercentDiscount => throw new NotImplementedException();
    }
}
