using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class RegisterViewModel : ViewModelBase<User>
    {
        [Display(Name = nameof(CommonText.Email), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationText.Email), ErrorMessageResourceType = typeof(ValidationText))]
        public string Email { get; set; }

        [Display(Name = nameof(CommonText.Password), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Password { get; set; }

        [Display(Name = nameof(CommonText.ConfirmPassword), ResourceType = typeof(CommonText))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ValidationText.Compare), ErrorMessageResourceType = typeof(ValidationText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string ConfirmPassword { get; set; }

        public override User ToModel()
        {
            return new User()
            {
                UserName = Email,
                Email = Email
            };
        }
    }
}
