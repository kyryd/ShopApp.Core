using ShopApp.Core.Models.Taxes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Taxes
{
    public class Tax20PercentMock : ITax
    {
        public string Name { get; init; } = "20 Percent VAT";
        public decimal Rate { get ; init; } = 20m;
    }
}
