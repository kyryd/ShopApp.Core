using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Models.Models
{
    public record Category(string Name, string Description) :Entity,  ICategory;
   
}
