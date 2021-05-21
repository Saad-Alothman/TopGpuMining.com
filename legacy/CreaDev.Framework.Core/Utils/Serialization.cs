using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CreaDev.Framework.Core.Utils
{
    public static class Serialization
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        public static string SerializeJavaScript<T>(T serializable)
        {

            return JsonConvert.SerializeObject(serializable, Settings);
        }
        public static string Serialize<T>(T serializable)
        {

            return JsonConvert.SerializeObject(serializable);
        }
        public static T DeSerialize<T>(string text)
        {

            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
