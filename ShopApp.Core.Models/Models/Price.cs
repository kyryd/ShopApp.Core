using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Models.Models
{
    public sealed record Price(ICurrency Currency, decimal Amount) : Entity, IPrice
    {
        public decimal Amount { get; set; } = Amount;
    }
}
