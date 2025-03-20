using ShopApp.Core.Models.Taxes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.BLoC.Taxes.Abstract
{
    public interface ITaxProvider
    {
        ITax Tax { get; init; }
    }
}
