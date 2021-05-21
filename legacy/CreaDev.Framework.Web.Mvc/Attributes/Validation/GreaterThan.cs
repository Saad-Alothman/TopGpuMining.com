using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation
{
    public class GreaterThan : ValidationAttribute, IClientValidatable
    {
        public string PropertyToCompare { get; set; }

        public GreaterThan(string propertyTo)
        {
            this.PropertyToCompare = propertyTo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propInfo = validationContext.ObjectType.GetProperty(this.PropertyToCompare);

            if (propInfo == null)
                throw new InvalidOperationException($"Cannot find property '{this.PropertyToCompare}' to compare");
            
            var propValue = propInfo.GetValue(validationContext.ObjectInstance, null);

            if (value is DateTime)
            {
                if ( (DateTime)value > (DateTime)propValue)
                    return ValidationResult.Success;
            }

            
            return new ValidationResult(base.ErrorMessage);
        }

        
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("propertytocompare", PropertyToCompare);
            rule.ValidationType = "greaterthanvld";
            
            yield return rule;
        }
    }
}
