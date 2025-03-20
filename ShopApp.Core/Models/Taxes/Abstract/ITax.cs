using ShopApp.Core.Models.Abstract;

namespace ShopApp.Core.Models.Taxes.Abstract
{
    public interface ITax
    {
        public string Name { get; init; }
        public decimal Rate { get; init; }
        //public ICurrency Currency { get; init; }
    }
}
