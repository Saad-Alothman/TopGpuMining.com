using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public interface ITabularDataViewModel
    {
        NameValueCollection ToTabularDataViewModel();
    }
}