using ShopApp.Core.Models.Models.Taxes.Abstract;

namespace ShopApp.Core.Logic.BLoC.Taxes.Abstract
{
    public interface ITaxProvider
    {
        ITax Tax { get; init; }
    }
}
