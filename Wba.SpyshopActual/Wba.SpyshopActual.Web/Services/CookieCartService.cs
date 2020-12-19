using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Shopping;

namespace Wba.SpyshopActual.Web.Services
{
    public class CookieCartService : ICartService
    {
        private const string Cart_CookieName = "SpyCart";

        protected HttpContext _context;
        protected Cart _cart;

        public CookieCartService(IHttpContextAccessor ctxAccessor)
        {
            _context = ctxAccessor.HttpContext;
        }

        public void AddToCart(int productID)
        {
            CartItem item = _cart.CartItems.FirstOrDefault(i => i.ProductID == productID);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                _cart.CartItems.Add(new CartItem { ProductID = productID, Quantity = 1 });
            }
        }

        public IEnumerable<CartItem> GetAll()
        {
           return _cart.CartItems;
        }

        public void LoadCart()
        {
            string cartSerialised = null;
            if(_context.Request.Cookies.TryGetValue(Cart_CookieName,out cartSerialised))
            {
                _cart = JsonConvert.DeserializeObject<Cart>(cartSerialised);
            }
            else
            {
                _cart = new Cart();
            }

        }

        public void SaveCart()
        {
            string cartSerialized = JsonConvert.SerializeObject(_cart);
            _context.Response.Cookies.Append(Cart_CookieName, cartSerialized, 
                new CookieOptions 
                {
                    HttpOnly = false, 
                    Expires = DateTimeOffset.Now.AddDays(30) 
                });
        }

        public void UpdateCartItem(int productID, int quantity)
        {
            CartItem item = _cart.CartItems.FirstOrDefault(i => i.ProductID == productID);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else 
                {
                    _cart.CartItems.Remove(item);
                }
            }
        }
    }
}
