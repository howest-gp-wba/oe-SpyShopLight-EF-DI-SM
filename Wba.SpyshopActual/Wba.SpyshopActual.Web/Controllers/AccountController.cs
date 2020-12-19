using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Web.ViewModels;

namespace Wba.SpyshopActual.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            var viewModel = new AccountLoginVM();
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Login(AccountLoginVM viewModel)
        {
            string validUser = "Joe";
            string validPass = "unsafe";

            if (ModelState.IsValid)
            {
                
                if (viewModel.UserName.Trim().Equals(validUser, StringComparison.InvariantCultureIgnoreCase)
                    && viewModel.PassWord==validPass)
                {
                    TempData[Constants.Constants.SuccesMessage] = $"Welcome, <b>{viewModel.UserName}</b> <br/> You may now log in.";
                    return new RedirectToActionResult("Index", "Home", null);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The credentials are unvalid");
                    return View(viewModel);
                }

            }
            else {
                return View(viewModel);
            }

            

        }


    }
}
