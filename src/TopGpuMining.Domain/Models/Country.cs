using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopGpuMining.Domain.Models
{
    public class Country : BaseEntity , ISeedableEntity<Country>
    {
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

        public Country()
        {

        }

        public Country Update(Country entity)
        {
            this.NameArabic = entity.NameArabic;
            this.NameEnglish = entity.NameEnglish;
            this.Area = entity.Area;
            this.Population = entity.Population;
            this.CapitalArabic = entity.CapitalArabic;
            this.CapitalEnglish = entity.CapitalEnglish;
            this.Longtitude = entity.Longtitude;
            this.Latitude = entity.Latitude;
            this.IsActive = entity.IsActive;

            return this;
        }
    }
}
