namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic
{
    public class ContainerColor
    {
        public static ContainerColor White = new ContainerColor("white");
        public static ContainerColor Bluesteel = new ContainerColor("blue-steel");
        public static ContainerColor Redmint = new ContainerColor("red-mint");
        public ContainerColor(string cssClass)
        {
            this.CssClass = cssClass;
        }
        public string CssClass;

    }
}