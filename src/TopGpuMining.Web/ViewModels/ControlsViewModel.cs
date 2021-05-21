using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class ControlsViewModel
    {
        [Display(Name = nameof(CommonText.StartDate), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public DateTime? StartDate { get; set; }

        [Display(Name = nameof(CommonText.EndDate), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public DateTime? EndDate { get; set; } = new DateTime(1985,9,2);

        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [Display(Name = nameof(CommonText.Enable), ResourceType = typeof(CommonText))]
        public bool Enabled { get; set; }


        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [Display(Name = nameof(CommonText.Gender), ResourceType = typeof(CommonText))]
        public Gender? Gender { get; set; }

        public Address Address { get; set; }

        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [Display(Name = nameof(CommonText.Mobile), ResourceType = typeof(CommonText))]
        public string Mobile { get; set; }
    }
}
