using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Web.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
