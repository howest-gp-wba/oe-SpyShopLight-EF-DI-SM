using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.SpyshopActual.Domain.Shopping
{
    public interface ICartService
    {
        void LoadCart();
        void SaveCart();
        IEnumerable<CartItem> GetAll();
        void AddToCart(int productID);
        void UpdateCartItem(int productID, int quantity);
    }
}
