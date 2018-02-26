using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Application.Amazon
{
    //https://github.com/tinohager/Nager.AmazonProductAdvertising
    public static class AmazonService
    {

        public  static void Search()
        {
            
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "AKIAI6YFV4IQDUUAN6MQ";
            authentication.SecretKey = "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";

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK, "saadtech-21");
            var result = wrapper.Search("MSI RX 580", AmazonSearchIndex.Electronics);

        }
    }
}
