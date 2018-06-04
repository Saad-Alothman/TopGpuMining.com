using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core;
using Nager.AmazonProductAdvertising;

namespace GpuMiningInsights.Domain.Models
{

    public static class PriceSourceTypeHelper
    {
        static Dictionary<PriceSourceType, AmazonEndpoint> endpointsDictionary = new Dictionary<PriceSourceType, AmazonEndpoint>()
        {
            {PriceSourceType.AmazonUs,AmazonEndpoint.US},
            {PriceSourceType.AmazonUk,AmazonEndpoint.UK},
            {PriceSourceType.AmazonCanada,AmazonEndpoint.CA},
            {PriceSourceType.AmazonIndia,AmazonEndpoint.IN},
        };
        public static AmazonEndpoint? ToAmazonEndpoint(PriceSourceType priceSource)
        {
            AmazonEndpoint? amazonEndpoint = null;
            if (priceSource.ToString().ToLower().StartsWith("amazon"))
            {
                //IF Following the same naming convention it will get it...
                string endpointText = priceSource.ToString().ToUpper().Replace("AMAZON", "");
                AmazonEndpoint amazonEndpointTest;
                if (Enum.TryParse(endpointText, out amazonEndpointTest))
                    amazonEndpoint = amazonEndpointTest;
                //otherwise fallback to the dictionary
                else
                {
                    Guard.AgainstFalse(endpointsDictionary.ContainsKey(priceSource),"Could Not Map Price source to amazon endpoin");
                    amazonEndpoint = endpointsDictionary[priceSource];
                }
            }

            return amazonEndpoint;
        }
    }
    public enum PriceSourceType
    {
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