using ShopApp.Core.Models.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Models.Stock.Abstract
{
    public interface IStock<P> where P : IProduct
    {
        public P Product { get; }
        public decimal NumberOfUnits { get; }
        public decimal ReservedUnits { get; }
        public decimal AvailableUnits { get; }
        public decimal ToDeliveryUnits { get;}
    }
}
