using System.Net;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.ActionResults
{
    
       public class HttpInternalServerErrorResult : HttpStatusCodeResult
    {
        public HttpInternalServerErrorResult()
            : this(null)
        {
        }

        // NotFound is equivalent to HTTP status 404.
        public HttpInternalServerErrorResult(string statusDescription)
            : base(HttpStatusCode.InternalServerError, statusDescription)
        {
        }
    }
}
