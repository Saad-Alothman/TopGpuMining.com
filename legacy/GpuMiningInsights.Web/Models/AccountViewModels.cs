using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Resources;

namespace CHRS.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = nameof(Common.Email), ResourceType = typeof(Common))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Common.Password), ResourceType = typeof(Common))]
        public string Password { get; set; }

        [Display(Name = nameof(Common.RememberMe), ResourceType = typeof(Common))]

        public bool RememberMe { get; set; }

    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

  
    


   
}
