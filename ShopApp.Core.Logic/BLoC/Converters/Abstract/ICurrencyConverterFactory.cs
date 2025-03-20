using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Logic.BLoC.Converters.Abstract
{
    public interface ICurrencyConverterFactory<CC> where CC : ICurrencyConverter
    {
        ICurrencyConverter GetNewInstance();
        ICurrencyConverter GetInstance();

    }
}
