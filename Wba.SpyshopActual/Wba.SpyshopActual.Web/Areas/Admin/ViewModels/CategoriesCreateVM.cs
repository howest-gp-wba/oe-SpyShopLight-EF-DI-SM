using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Web.Areas.Admin.ViewModels
{
    public class CategoriesCreateVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int NumberOfProducts { get; set; }

    }
}
