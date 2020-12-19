using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Web.ViewModels
{
    public class CartIndexVM
    {
        public CartIndexVM()
        {
            Items = new List<CartItemVM>();
        }

        public IList<CartItemVM> Items { get; set; }
        public decimal CartTotal { get { return Items.Sum(i => i.ProductTotal); } } 
    }
}
