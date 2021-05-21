using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class CountryViewModel : ViewModelBase<Country>
    {
        public string Id { get; set; }

        public string CodeTwo { get; set; }

        [Display(Name = nameof(CommonText.NameArabic), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string NameArabic { get; set; }

        [Display(Name = nameof(CommonText.NameEnglish), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string NameEnglish { get; set; }

        [Display(Name = nameof(CommonText.PhoneCode), ResourceType = typeof(CommonText))]
        public string PhoneCode { get; set; }

        [Display(Name = nameof(CommonText.Enable), ResourceType = typeof(CommonText))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(CommonText.CapitalEnglish), ResourceType = typeof(CommonText))]
        public string CapitalEnglish { get; set; }

        [Display(Name = nameof(CommonText.CapitalArabic), ResourceType = typeof(CommonText))]
        public string CapitalArabic { get; set; }

        [Display(Name = nameof(CommonText.Population), ResourceType = typeof(CommonText))]
        public int? Population { get; set; }

        [Display(Name = nameof(CommonText.Latitude), ResourceType = typeof(CommonText))]
        public float? Latitude { get; set; }

        [Display(Name = nameof(CommonText.Longitude), ResourceType = typeof(CommonText))]
        public float? Longtitude { get; set; }

        [Display(Name = nameof(CommonText.Area), ResourceType = typeof(CommonText))]
        public double? Area { get; set; }

        public CountryViewModel()
        {

        }

        public CountryViewModel(Country entity)
        {
            Id = entity.Id;
            Area = entity.Area;
            CapitalArabic = entity.CapitalArabic;
            CapitalEnglish = entity.CapitalEnglish;
            CodeTwo = entity.CodeTwo;
            IsActive = entity.IsActive;
            Latitude = entity.Latitude;
            Longtitude = entity.Longtitude;
            NameArabic = entity.NameArabic;
            NameEnglish = entity.NameEnglish;
            PhoneCode = entity.PhoneCode;
            Population = entity.Population;
        }

        public override Country ToModel()
        {
            
            var result = new Country()
            {
                Id = Id,
                Area = Area,
                CapitalArabic = CapitalArabic,
                CapitalEnglish = CapitalEnglish,
                CodeTwo = CodeTwo,
                IsActive = IsActive,
                Latitude = Latitude,
                Longtitude = Longtitude,
                NameArabic = NameArabic,
                NameEnglish = NameEnglish,
                PhoneCode = PhoneCode,
                Population = Population
            };

            return result;
        }

    }
}
