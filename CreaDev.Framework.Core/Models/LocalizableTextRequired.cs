using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Models
{
    [ComplexType]
    [Serializable]
    public class LocalizableTextRequired
    {

        [Display(ResourceType = typeof(Common), Name = nameof(Common.Arabic))]
        [Required(ErrorMessageResourceName = nameof(Core.Resources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Core.Resources.ErrorMessages))]
        public string Arabic { get; set; }

        [Display(ResourceType = typeof(Common), Name = nameof(Common.English))]
        [Required(ErrorMessageResourceName = nameof(Core.Resources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Core.Resources.ErrorMessages))]
        public string English { get; set; }

        public LocalizableTextRequired()
        {

        }

        public LocalizableTextRequired(string arabic, string english)
        {
            this.Arabic = arabic;
            this.English = english;
        }
        //TO Pass the property when Serialized
        public string LocalizedText
        {
            get
            {
                if (Culture.IsEnglish)
                    return English;
                else
                    return Arabic;
            }
        }

        public override string ToString()
        {
            return LocalizedText;
        }

    }
}