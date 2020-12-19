using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Web.Areas.Admin.ViewModels
{
    public class ProductDetailVM : ProductsEditVM
    {
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
