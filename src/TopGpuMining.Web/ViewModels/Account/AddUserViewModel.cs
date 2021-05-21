using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class AddUserViewModel : UserViewModel
    {
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public override string Password { get => base.Password; set => base.Password = value; }

        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public override string ConfirmPassword { get => base.ConfirmPassword; set => base.ConfirmPassword = value; }

        public AddUserViewModel()
        {

        }

        public AddUserViewModel(User user) : base(user)
        {

        }
    }
}
