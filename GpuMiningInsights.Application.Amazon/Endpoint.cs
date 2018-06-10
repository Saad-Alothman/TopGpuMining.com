using Nager.AmazonProductAdvertising;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Application.Amazon
{
    public static class Endpoints
    {
        private static List<Endpoint> _endpoints;
        public static List<Endpoint> EndpointsList
        {
            get
            {
                if (_endpoints == null)
                    LoadEndpoints();
                return _endpoints;
            }
        }

        private static void LoadEndpoints()
        {
            string endpointsJson = "[\r\n  {\r\n    \"accessKey\": \"AKIAI6YFV4IQDUUAN6MQ\",\r\n    \"secreteKey\": \"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\",\r\n    \"endpointCountryName\": \"US\",\r\n    \"endpointCode\": \"US\",\r\n    \"merchantId\": \"saadtech0d-20\"\r\n\r\n  },\r\n  {\r\n    \"accessKey\": \"AKIAI6YFV4IQDUUAN6MQ\",\r\n    \"secreteKey\": \"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\",\r\n    \"endpointCountryName\": \"UK\",\r\n    \"endpointCode\": \"UK\",\r\n    \"merchantId\": \"saadtech-21\"\r\n  },\r\n  {\r\n    \"accessKey\": \"AKIAI6YFV4IQDUUAN6MQ\",\r\n    \"secreteKey\": \"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\",\r\n    \"endpointCountryName\": \"India\",\r\n    \"endpointCode\": \"IN\",\r\n    \"merchantId\": \"saadtech05-21\"\r\n\r\n  },\r\n  {\r\n    \"accessKey\": \"AKIAI6YFV4IQDUUAN6MQ\",\r\n    \"secreteKey\": \"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\",\r\n    \"endpointCountryName\": \"CA\",\r\n    \"endpointCode\": \"CA\",\r\n    \"merchantId\": \"saadtech-20\"\r\n  }\r\n]";
            _endpoints = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Endpoint>>(endpointsJson);
        }

        public static Endpoint Get(AmazonEndpoint amazonEndpoint)
        {
                Endpoint endpoint = EndpointsList.FirstOrDefault(e=>e.endpointCode.ToUpper() == amazonEndpoint.ToString().ToUpper());

            if (endpoint == null)
                throw new KeyNotFoundException();
            return endpoint;
        }
    }


    public class Endpoint
    {
        public string accessKey { get; set; }
        public string secreteKey { get; set; }
        public string endpointCountryName { get; set; }
        public string endpointCode { get; set; }
        public string merchantId { get; set; }
    }
}
