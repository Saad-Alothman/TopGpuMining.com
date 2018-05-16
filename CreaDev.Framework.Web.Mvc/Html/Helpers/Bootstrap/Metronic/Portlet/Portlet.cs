using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public class Portlet : DivElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public Portlet(ViewContext viewContext) : base(viewContext)
        {
        }

        public override void OnEnding(ViewContext viewContext)
        {
         
        }

    }
    public class PortletHeader : DivElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public PortletHeader(ViewContext viewContext) : base(viewContext)
        {
        }

        public override void OnEnding(ViewContext viewContext)
        {

        }

    }
    public class PortletHeaderActions : DivElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public PortletHeaderActions(ViewContext viewContext) : base(viewContext)
        {
        }

        public override void OnEnding(ViewContext viewContext)
        {

        }

    }
    public class PortletBody : DivElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public PortletBody(ViewContext viewContext) : base(viewContext)
        {
        }

        public override void OnEnding(ViewContext viewContext)
        {

        }

    }
}