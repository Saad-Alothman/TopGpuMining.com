using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopGpuMining.Domain.Models
{
    public class Address : AuditableEntity , ISeedableEntity<Address>
    {
        [Display(Name = nameof(CommonText.FirstName), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string FirstName { get; set; }

        [Display(Name = nameof(CommonText.LastName), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string LastName { get; set; }

        [Display(Name = nameof(CommonText.PostalCode), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string PostalCode { get; set; }

        [Display(Name = nameof(CommonText.Email), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationText.Email), ErrorMessageResourceType = typeof(ValidationText))]
        public string Email { get; set; }

        [Display(Name = nameof(CommonText.Mobile), ResourceType = typeof(CommonText))]
        [RegularExpression(RegExHelper.SAUDI_MOBILE, ErrorMessageResourceName = nameof(ValidationText.SaudiMobile),ErrorMessageResourceType = typeof(ValidationText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Mobile { get; set; }

        [Display(Name = nameof(CommonText.Country), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        [Display(Name = nameof(CommonText.Street), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Street { get; set; }

        [Display(Name = nameof(CommonText.City), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string City { get; set; }

        [Display(Name = nameof(CommonText.State), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string State { get; set; }

        public Address Update(Address entity)
        {
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
            this.PostalCode = entity.PostalCode;
            this.Mobile = entity.Mobile;
            this.State = entity.State;
            this.Street = entity.Street;
            this.CountryId = entity.CountryId;
            this.Email = entity.Email;

            return this;
        }
    }
}
