using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class ResponseExtension
    {
        public static void SetBadRequest(this HttpResponseBase httpResponseBase)
        {
            httpResponseBase.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
