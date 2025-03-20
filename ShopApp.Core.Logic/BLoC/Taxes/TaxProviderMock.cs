using ShopApp.Core.Logic.BLoC.Taxes.Abstract;
using ShopApp.Core.Models.Models.Taxes;
using ShopApp.Core.Models.Models.Taxes.Abstract;

namespace ShopApp.Core.Logic.BLoC.Taxes
{
    public class TaxProviderMock : ITaxProvider
    {
        public ITax Tax { get; init; } = new Tax20PercentMock();
    }
}
