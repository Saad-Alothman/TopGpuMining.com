using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Models
{
    [ComplexType]
    [Serializable]
    public class LocalizableText
    {

        [Display(ResourceType = typeof(Common), Name = nameof(Common.Arabic))]
        public string Arabic { get; set; }


        [Display(ResourceType = typeof(Common), Name = nameof(Common.English))]
        public string English { get; set; }

        public LocalizableText()
        {

        }

        public LocalizableText(string arabic, string english)
        {
            this.Arabic = arabic;
            this.English = english;
        }

        public override string ToString()
        {
            if (Culture.IsEnglish)
                return English;
            else
                return Arabic;
        }

    }
}