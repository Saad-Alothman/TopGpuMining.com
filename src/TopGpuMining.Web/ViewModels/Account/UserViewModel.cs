using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class UserViewModel : ViewModelBase<User>
    {
        public string UserId { get; set; }

        [Display(Name = nameof(CommonText.Email), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationText.Email), ErrorMessageResourceType = typeof(ValidationText))]
        public string Email { get; set; }

        [Display(Name = nameof(CommonText.Password), ResourceType = typeof(CommonText))]
        
        public virtual string Password { get; set; }

        [Display(Name = nameof(CommonText.ConfirmPassword), ResourceType = typeof(CommonText))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ValidationText.Compare), ErrorMessageResourceType = typeof(ValidationText))]
        public virtual string ConfirmPassword { get; set; }

        public string[] Roles { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(User user)
        {
            this.Email = user.Email;
            this.UserId = user.Id;
            this.Roles = user.Roles.Select(a => a.RoleId).ToArray();
        }

        public override User ToModel()
        {
            var result = new User
            {
                Id = UserId,
                UserName = Email,
                Email = Email
            };

            if (Roles != null)
            {
                foreach (var item in Roles)
                {
                    result.Roles.Add(new IdentityUserRole<string>()
                    {
                        RoleId = item
                    });
                }
            }

            return result;
        }
    }
}
