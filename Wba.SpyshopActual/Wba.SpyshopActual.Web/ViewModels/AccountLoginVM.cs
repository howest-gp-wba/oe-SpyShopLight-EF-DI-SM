using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Web.ViewModels
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage ="Geef iet in")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }

    }
}
