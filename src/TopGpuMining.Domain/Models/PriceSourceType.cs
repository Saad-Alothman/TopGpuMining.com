using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TopGpuMining.Domain.Models
{

    
    public enum PriceSourceType
    {
        NotSet=0,
        [Display(Name = "Amazon USA")]
        AmazonUs = 1,
        [Display(Name = "Amazon UK")]
        AmazonUk = 2,
        [Display(Name = "Amazon Canada")]
        AmazonCanada = 3,
        [Display(Name = "Amazon India")]
        AmazonIndia = 4,
        [Display(Name = "New Egg")]
        NewEgg = 5
    }
    /*
     
     */
}