using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wba.SpyshopActual.Web.Models;
using Wba.SpyshopActual.Web.ViewModels;

namespace Wba.SpyshopActual.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            // Create the View Model
            var viewModel = new HomeAboutVM()
            {
                ContactEmail = "info@spyshop.example",
                CompanyFullName = "Spy Shop Incorperated",
                AboutTitle = "Welcome to Spy Shop",
                AboutContent = "<p>We deliver permium Gadgets ot help all Clouseaus and Bonds out there.<br/> To start have a look at the <a href='/'> homepage </a> !  </p> "
            };
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
