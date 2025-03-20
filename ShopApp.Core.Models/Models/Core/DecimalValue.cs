using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Models.Models.Core
{
    public sealed record DecimalValue(decimal Value, ValueCategory Category) : Entity, IDecimalValue;
}
