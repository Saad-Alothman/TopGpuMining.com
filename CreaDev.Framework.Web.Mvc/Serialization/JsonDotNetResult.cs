using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CreaDev.Framework.Web.Mvc.Serialization
{
    public class JsonDotNetResult : JsonResult
    {
        //javascriptVariableConvention
        public bool CamelCasing { get; set; } = true;

        public JsonDotNetResult()
        {

            
            if (CamelCasing)
            {
                Settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new StringEnumConverter() },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 5,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    Culture = CultureInfo.CurrentUICulture,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            }
            else
            {
                Settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new StringEnumConverter() },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    Culture = CultureInfo.CurrentUICulture
                };
            }
        }

        public JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            Culture = CultureInfo.CurrentUICulture
        };
        

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET request not allowed");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            response.Write(JsonConvert.SerializeObject(this.Data, Settings));
        }
    }
}
