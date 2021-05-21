using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.Helpers
{
    public class GreaterThanDate : ValidationAttribute, IClientModelValidator
    {
        private ClientModelValidationContext _context;

        public string PropertyToCompare { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance;
            var type = validationContext.ObjectInstance.GetType();

            var valueToCompate = type.GetProperty(PropertyToCompare)?.GetValue(obj, null);

            if (valueToCompate == null || value == null)
                return ValidationResult.Success;

            if (valueToCompate is DateTime compareDate && value is DateTime srcDate)
            {
                if (srcDate >= compareDate)
                    return ValidationResult.Success;
            }



            return new ValidationResult(ErrorMessage);

        }
        public void AddValidation(ClientModelValidationContext context)
        {
            _context = context;

            var errorMsg = FormatErrorMessage(_context.ModelMetadata.DisplayName);

            _context.Attributes.Add("data-val", "true");
            _context.Attributes.Add("data-val-greaterthanvld", errorMsg);
            _context.Attributes.Add("data-val-greaterthanvld-propertyToCompare", PropertyToCompare);

        }


    }
}
