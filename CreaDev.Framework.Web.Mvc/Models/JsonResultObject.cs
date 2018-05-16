using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public class JsonResultObject
    {
        public bool IsRedirect { get; set; }

        public string RedirectUrl { get; set; }

        public bool Success { get; set; }

        public Alert AlertMessage { get; set; }

        public string PartialViewHtml { get; set; }
        public string Data { get; set; }

        public JsonResultObject()
        {
            this.IsRedirect = false;
            this.Success = true;
        }

        public JsonResultObject(Alert model)
        {
            this.IsRedirect = false;
            this.Success = model.AlertType != Alert.Type.Error;
            this.AlertMessage = model;
        }
    }
}
