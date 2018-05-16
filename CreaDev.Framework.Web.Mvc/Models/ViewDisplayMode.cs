using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public enum ViewDisplayMode
    {
        DisabledWithEditButton,
        EnabledWihtUpdateButton,
        EnabledNoButtons
    }
    public enum SubmitType
    {
        Edit = 1,
        Cancel = 2
    }
}
