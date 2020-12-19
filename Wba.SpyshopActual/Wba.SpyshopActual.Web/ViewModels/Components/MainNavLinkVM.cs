using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Web.ViewModels.Components
{
    public class MainNavLinkVM
    {
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        
        public string Area { get; set; }
        public bool IsActive { get; set; }
        
    }
}
