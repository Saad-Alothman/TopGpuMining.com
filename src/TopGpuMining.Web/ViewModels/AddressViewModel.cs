using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class AddressViewModel : ViewModelBase<Address>
    {
        public string Id { get; set; }

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
        [RegularExpression(RegExHelper.SAUDI_MOBILE, ErrorMessageResourceName = nameof(ValidationText.SaudiMobile), ErrorMessageResourceType = typeof(ValidationText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Mobile { get; set; }

        [Display(Name = nameof(CommonText.Country), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string CountryId { get; set; }
        
        [Display(Name = nameof(CommonText.Street), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Street { get; set; }

        [Display(Name = nameof(CommonText.City), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string City { get; set; }

        [Display(Name = nameof(CommonText.State), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string State { get; set; }
        
        public AddressViewModel()
        {

        }

        public AddressViewModel(Address entity)
        {
            if (entity == null)
                return;

            this.City = entity.City;
            this.CountryId = entity.CountryId;
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
            this.State = entity.State;
            this.Street = entity.Street;
            this.PostalCode = entity.PostalCode;
            this.Mobile = entity.Mobile;
            this.Email = entity.Email;
            this.Id = entity.Id;
        }

        public override Address ToModel()
        {
            return new Address()
            {
                City = City,
                State = State,
                Street = Street,
                CountryId = CountryId,
                Mobile = Mobile,
                LastName = LastName,
                FirstName = FirstName,
                PostalCode = PostalCode,
                Email = Email,
                Id = Id
            };
        }

    }
}
