using ShopApp.Core.Models.Models.Address.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Models.Address
{
    public record Address(string Street, string City, string State, string Zip, string Country) : Entity, IAddress;
}
