using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wba.SpyshopActual.Domain;
using Wba.SpyshopActual.Domain.Entitys;
using Wba.SpyshopActual.Domain.Repositorys;
using Wba.SpyshopActual.Web.Data;
using Wba.SpyshopActual.Web.ViewModels;

namespace Wba.SpyshopActual.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product,int> _pRepository ;

        public ProductController(IRepository<Product, int> pRepository)
        {
            _pRepository = pRepository;

        }

        public IActionResult Index()
        {
            var viewModel = new ProductIndexVM();
            viewModel.Products = _pRepository.GetAll()
                                    .Include(p=>p.Category).ToList();

            return View(viewModel);
        }

        
        public IActionResult ProductDetail(int prodID)
        {
            var viewModel = new ProductIndexVM();
            viewModel.Products = _pRepository.GetAll().Include(p=>p.Category).ToList();

            var gekozenProduct = viewModel.Products.SingleOrDefault(p => p.Id == prodID);


            return View(gekozenProduct);
        }

        
    }
}
