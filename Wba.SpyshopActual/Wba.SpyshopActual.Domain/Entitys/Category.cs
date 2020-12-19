using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.SpyshopActual.Domain.Entitys
{
    public class Category : BaseEntity<int>
    {
        
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
