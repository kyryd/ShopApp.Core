using ShopApp.Core.Enums;
using ShopApp.Core.Models.Core.Abstract;

namespace ShopApp.Core.Models.Core
{
    public sealed record DecimalValue(decimal Value, ValueCategory Category) : Entity, IDecimalValue;
}
