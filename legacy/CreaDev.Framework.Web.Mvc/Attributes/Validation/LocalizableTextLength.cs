using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation
{
    public class LocalizableTextLength : ValidationAttribute
    {
        public int MaxLength { get; set; }
        public LocalizableTextLength(int maximumLength)
        {
            MaxLength = maximumLength;
        }

        public override bool IsValid(object value)
        {

            if (value is LocalizableText)
            {
                LocalizableText localizableText = value as LocalizableText;
                return localizableText?.Arabic?.Length <= this.MaxLength && localizableText?.English?.Length <= this.MaxLength;
            }
            else if (value is LocalizableTextRequired)
            {
                LocalizableTextRequired localizableText = value as LocalizableTextRequired;
                return localizableText.Arabic?.Length <= this.MaxLength && localizableText.English?.Length <= this.MaxLength;
            }


            return true;
        }
    }

}
