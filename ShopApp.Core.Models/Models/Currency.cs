using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Models.Models
{
    public sealed record Currency(Currencies Value) :Entity, ICurrency;
}
