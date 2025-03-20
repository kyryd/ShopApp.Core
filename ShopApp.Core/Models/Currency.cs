using ShopApp.Core.Enums;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models
{
    public sealed record Currency(Currencies Value) :Entity, ICurrency;
}
