using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.SpyshopActual.Domain.Entitys
{
    public class Product : BaseEntity<int>
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public int? SortNumber { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
