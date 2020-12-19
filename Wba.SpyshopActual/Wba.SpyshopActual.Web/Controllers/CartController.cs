using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain;
using Wba.SpyshopActual.Domain.Entitys;
using Wba.SpyshopActual.Domain.Shopping;
using Wba.SpyshopActual.Web.ViewModels;

namespace Wba.SpyshopActual.Web.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;
        IRepository<Product, int> _pRepository;

        public CartController(ICartService cartService, IRepository<Product, int> pRepository)
        {
            _cartService = cartService;
            _pRepository = pRepository;
        }
        
        
        public async Task<IActionResult> Index()
        {
            _cartService.LoadCart();

            var cartItems = _cartService.GetAll();
            var products = await _pRepository.GetAll()
                                .Where(p => cartItems
                                .Select(cp => cp.ProductID)
                                .Contains(p.Id)).ToListAsync();

            var viewModel = new CartIndexVM();
            
            foreach (var cartItem in cartItems) 
            { 
                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductID);
                if (product == null) 
                { 
                    continue; 
                } 
                viewModel.Items.Add(new CartItemVM 
                { 
                    ProductId = product.Id, 
                    Name = product.Name, 
                    Quantity = cartItem.Quantity, 
                    UnitPrice = product.Price 
                }); 
            }


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(CartIndexVM inputCart)
        {
            _cartService.LoadCart();
            foreach (var ciVM in inputCart.Items) 
            {
                _cartService.UpdateCartItem(ciVM.ProductId, ciVM.Quantity);
            }
            _cartService.SaveCart();
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(int prodID)
        {
            var product = await _pRepository.GetByIdAsync(prodID);
            if (product != null)
            {
                _cartService.LoadCart();
                _cartService.AddToCart(product.Id);
                _cartService.SaveCart();

                TempData[Constants.Constants.SuccesMessage] = $"Product {product.Name} has been added to your Shopping Cart";
            }
            return RedirectToAction("ProductDetail", new { Controller = "Product", prodID });
        }
    }
}
