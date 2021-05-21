using System.ComponentModel.DataAnnotations;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation
{
    public class CreaDevRequiredAttribute : RequiredAttribute
    {
        //[Required(ErrorMessageResourceName = nameof(CreaDev.Framework.Core.Resources.ErrorMessages.Required), )]

        public CreaDevRequiredAttribute() : base()
        {
            this.ErrorMessageResourceType = typeof(CreaDev.Framework.Core.Resources.ErrorMessages);
            this.ErrorMessageResourceName = nameof(CreaDev.Framework.Core.Resources.ErrorMessages.Required);
        }
    }
}