using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation
{
    public class ArabicInputAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public ArabicInputAttribute() : base(RegularExpressions.ArabicWithEnglishNumbers)
        {
            this.ErrorMessage = ErrorMessages.ArabicInputValidationMessage;
            
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRegexRule(ErrorMessage, Pattern);
            return new List<ModelClientValidationRegexRule> { modelClientValidationRule };
        }
    }
}
