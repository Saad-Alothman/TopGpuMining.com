namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public class PortletType
    {
        public static PortletType Boxed = new PortletType("boxed");
        public static PortletType Light = new PortletType("light");
        public static PortletType Solid = new PortletType("solid");
        public static PortletType Draggable = new PortletType("blue-steel");

        public PortletType(string cssClass)
        {
            this.CssClass = cssClass;
        }
        public string CssClass;

    }
}