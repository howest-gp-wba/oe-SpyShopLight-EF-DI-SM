using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.SpyshopActual.Domain.Shopping
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public IList<CartItem> CartItems { get; set; }

    }
}
