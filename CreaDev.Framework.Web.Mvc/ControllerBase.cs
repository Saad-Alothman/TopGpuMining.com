using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using CreaDev.Framework.Core.Exceptions;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;
using CreaDev.Framework.Web.Mvc.ActionResults;
using CreaDev.Framework.Web.Mvc.Filters;
using CreaDev.Framework.Web.Mvc.Helpers;
using CreaDev.Framework.Web.Mvc.Helpers.CollectionVariableNames;
using CreaDev.Framework.Web.Mvc.Models;
using CreaDev.Framework.Web.Mvc.Serialization;

namespace CreaDev.Framework.Web.Mvc
{
    
    [AutoErrorLogger(typeof(ControllerBase), true)]
    [Internationalization]
    public class ControllerBase : Controller
    {

           }

}
