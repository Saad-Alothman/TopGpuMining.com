using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Web.Mvc.Binders
{
    public class CreaDevDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var date = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (String.IsNullOrEmpty(date))
                return null;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));

            DateTime result;
            var success = DateTime.TryParseExact(date, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out result);

            if (success)
                return result;
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format(ErrorMessages.CreaDevDateTimeFormat, bindingContext.ModelName));
                return null;
            }
        }

    }
}
