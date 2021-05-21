using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Resources;
using CreaDev.Framework.Web.Mvc.Attributes.Validation;

namespace CreaDev.Framework.Web.Mvc.Models
{
    //[ComplexType]
    //public class LocalizableText
    //{
    //    [ArabicInput]
    //    [Display(ResourceType = typeof(Common), Name =nameof(Common.Arabic))]
    //    public string Arabic { get; set; }
    //    [EnglishInput]
    //    [Display(ResourceType = typeof(Common), Name = nameof(Common.English))]

    //    public string English { get; set; }

    //    [NotMapped]
    //    public string LocalizedValue => Framework.Core.Culture.GetThreadLanguageValue(Arabic, English);

    //    public LocalizableText()
    //    {

    //    }

    //    public LocalizableText(string arabic, string english)
    //    {
    //        this.Arabic = arabic;
    //        this.English = english;
    //    }

    //    public override string ToString()
    //    {
    //        return LocalizedValue;
    //    }
    //}

    //[ComplexType]
    //public class LocalizableTextRequired
    //{
    //    [ArabicInput]
    //    [Required(ErrorMessageResourceName = nameof(ErrorMessages.Required), ErrorMessageResourceType = typeof(ErrorMessages))]
    //    [Display(ResourceType = typeof(Common), Name = nameof(Common.Arabic))]
    //    public string Arabic { get; set; }
    //    [Required(ErrorMessageResourceName = nameof(ErrorMessages.Required), ErrorMessageResourceType = typeof(ErrorMessages))]
    //    [EnglishInput]
    //    [Display(ResourceType = typeof(Common), Name = nameof(Common.English))]

    //    public string English { get; set; }

    //    [NotMapped]
    //    public string LocalizedValue => Framework.Core.Culture.GetThreadLanguageValue(Arabic, English);

    //    public LocalizableTextRequired()
    //    {

    //    }

    //    public LocalizableTextRequired(string arabic, string english)
    //    {
    //        this.Arabic = arabic;
    //        this.English = english;
    //    }

    //    public override string ToString()
    //    {
    //        return LocalizedValue;
    //    }
    //}
}
