using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreaDev.Framework.Web.Mvc
{
    public class PartialViewsDependancies
    {
        public PartialViewsDependancies()
        {
            this.Scripts = new List<string>();
            this.Styles= new List<string>();
        }
        private List<string> Scripts { get; set; }
        private List<string> Styles { get; set; }
        public void AddScript(string scriptTag)
        {
            if (!Scripts.Contains(scriptTag))
                Scripts.Add(scriptTag);
            SetToRequest(this);
        }
        public void AddStyle(string scriptTag)
        {
            if (!Styles.Contains(scriptTag))
                Styles.Add(scriptTag);
            SetToRequest(this);
        }

        public static PartialViewsDependancies GetFromRequest()
        {
            PartialViewsDependancies partialViewsDependancies =HttpContext.Current.Items["PartialViewsDependancies"] as PartialViewsDependancies;
            if (partialViewsDependancies == null)
            {
                partialViewsDependancies = new PartialViewsDependancies();

                SetToRequest(partialViewsDependancies);
            }

            return partialViewsDependancies;
        }
        public static void SetToRequest(PartialViewsDependancies  partialViewsDependancies)
        {
            HttpContext.Current.Items["PartialViewsDependancies"] = partialViewsDependancies;
        }

        public static string RenderScripts()
        {

            PartialViewsDependancies partialViewsDependancies =GetFromRequest();
            return partialViewsDependancies.Scripts.Aggregate("", (current, script) => current + (script + " <br />"));
        }
        public static string RenderStyles()
        {

            PartialViewsDependancies partialViewsDependancies = GetFromRequest();
            return partialViewsDependancies.Styles.Aggregate("", (current, script) => current + (script + " \r\n"));
        }

    }
}