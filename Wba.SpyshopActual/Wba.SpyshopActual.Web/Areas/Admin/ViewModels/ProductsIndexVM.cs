using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Web.Areas.Admin.ViewModels
{
    public class ProductsIndexVM
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
