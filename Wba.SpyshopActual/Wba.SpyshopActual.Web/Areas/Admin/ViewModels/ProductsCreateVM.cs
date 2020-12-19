using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Web.Areas.Admin.ViewModels
{
    public class ProductsCreateVM
    {
        [Required , MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public int? SortNumber { get; set; }

        public int? CategoryId { get; set; }

        public IEnumerable<Category> AvailableCategories{ get; set; }

        public IFormFile UploadedImage { get; set; }

    }
}
