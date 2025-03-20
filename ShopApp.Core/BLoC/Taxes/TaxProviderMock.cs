using ShopApp.Core.BLoC.Taxes.Abstract;
using ShopApp.Core.Models.Taxes;
using ShopApp.Core.Models.Taxes.Abstract;

namespace ShopApp.Core.BLoC.Taxes
{
    public class TaxProviderMock : ITaxProvider
    {
        public ITax Tax { get; init; } = new Tax20PercentMock();
    }
}
