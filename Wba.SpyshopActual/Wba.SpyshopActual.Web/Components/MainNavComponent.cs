using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Web.ViewModels.Components;

namespace Wba.SpyshopActual.Web.Components
{
    [ViewComponent(Name ="MainNavigation")]
    public class MainNavComponent : ViewComponent
    {
        private IEnumerable<MainNavLinkVM> publicLinks;

        public MainNavComponent()
        {
            publicLinks = new List<MainNavLinkVM> 
            {
                new MainNavLinkVM{Area=null, Controller="Home", Action="Index", Text="Start",IsActive=false },
                new MainNavLinkVM{Area=null,Controller="Home", Action="About", Text="About",IsActive=false },
                new MainNavLinkVM{Area=null,Controller="Home", Action="Contact", Text="Contact",IsActive=false },
                new MainNavLinkVM{Area=null,Controller="Product", Action="Index", Text="Products",IsActive=false },
                new MainNavLinkVM{Area=null,Controller="Account", Action="Login", Text="Login", IsActive=false },
                new MainNavLinkVM{Area="Admin",Controller="Products", Action="Index", Text="Modify", IsActive=false }
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navLinks = publicLinks;

            foreach (var lnk in navLinks)
            {
                if (    this.RouteData.Values["Controller"]?.ToString().ToLower() == lnk.Controller.ToLower() &&       this.RouteData.Values["Action"]?.ToString().ToLower() == lnk.Action.ToLower())
                {
                    lnk.IsActive = true;
                }
                else
                {
                    lnk.IsActive = false;
                }


            }

            return await Task.FromResult<IViewComponentResult>(View(navLinks));

        }


    }
}
