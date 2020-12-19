using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Shopping;

namespace Wba.SpyshopActual.Web.Components
{
    [ViewComponent(Name ="CartWidget")]
    public class CartWidgetComponent : ViewComponent
    {
        ICartService _cartService;
        public CartWidgetComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _cartService.LoadCart();
            int itemsInCart = _cartService.GetAll().Sum(i=>i.Quantity);
            await Task.Delay(0);
            return View(itemsInCart);

        }

    }
}
