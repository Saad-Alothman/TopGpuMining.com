using System;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public abstract class DivElement : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public abstract void OnEnding(ViewContext viewContext);

        public DivElement(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                EndForm(_viewContext);
            }
        }
        //public static void EndForm(this HtmlHelper htmlHelper)
        //{
        //    EndForm(htmlHelper.ViewContext);
        //}

        internal  void EndForm(ViewContext viewContext)
        {
            // close Div
            viewContext.Writer.Write("</div>");
            OnEnding(viewContext);
            
        }

        public void EndForm()
        {
            Dispose(true);
        }
    }
    public abstract class DivPanelElement : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        public abstract void OnEnding(ViewContext viewContext);

        public DivPanelElement(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                EndForm(_viewContext);
            }
        }
        //public static void EndForm(this HtmlHelper htmlHelper)
        //{
        //    EndForm(htmlHelper.ViewContext);
        //}

        internal void EndForm(ViewContext viewContext)
        {
            // close panel body
            viewContext.Writer.Write("</div>");
            OnEnding(viewContext);
            // close panel 
            viewContext.Writer.Write("</div>");
            viewContext.OutputClientValidation();
        }

        public void EndForm()
        {
            Dispose(true);
        }
    }
    public class Row : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public Row(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                EndDiv(_viewContext);
            }
        }
        //public static void EndForm(this HtmlHelper htmlHelper)
        //{
        //    EndForm(htmlHelper.ViewContext);
        //}

        internal static void EndDiv(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
            viewContext.OutputClientValidation();
        }

        public void EndDiv()
        {
            Dispose(true);
        }
    }
    public class Column : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public Column(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                EndDiv(_viewContext);
            }
        }
        //public static void EndForm(this HtmlHelper htmlHelper)
        //{
        //    EndForm(htmlHelper.ViewContext);
        //}

        internal static void EndDiv(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
            viewContext.OutputClientValidation();
        }

        public void EndDiv()
        {
            Dispose(true);
        }
    }

    public class FormGroup : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public FormGroup(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                EndDiv(_viewContext);
            }
        }
        //public static void EndForm(this HtmlHelper htmlHelper)
        //{
        //    EndForm(htmlHelper.ViewContext);
        //}

        internal static void EndDiv(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
            viewContext.OutputClientValidation();
        }

        public void EndDiv()
        {
            Dispose(true);
        }
    }
    public class Panel : DivPanelElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        private bool _isWithFooter ;
        private string[] _footerAnchors;
        public Panel(ViewContext viewContext,params string[] footerAnchors):base(viewContext)
        {
            _isWithFooter = footerAnchors != null && footerAnchors.Length > 0;
            _footerAnchors = footerAnchors;
        }

        public override void OnEnding(ViewContext viewContext)
        {
            if (_isWithFooter)
            {
                viewContext.Writer.Write(@"<div class=""panel-footer"">");
                if (_footerAnchors != null && _footerAnchors.Length > 0)
                {
                    foreach (var footerAnchor in _footerAnchors)
                    {

                        viewContext.Writer.Write(footerAnchor);

                    }
                }
                viewContext.Writer.Write("</div>");

            }
        }
        
    }
    public class Accordion : DivElement
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        private bool _isWithFooter;
        private string[] _footerAnchors;
        public Accordion(ViewContext viewContext, params string[] footerAnchors) : base(viewContext)
        {
            _isWithFooter = footerAnchors != null && footerAnchors.Length > 0;
            _footerAnchors = footerAnchors;
        }

        public override void OnEnding(ViewContext viewContext)
        {
            if (_isWithFooter)
            {
                viewContext.Writer.Write(@"<div class=""panel-footer"">");
                if (_footerAnchors != null && _footerAnchors.Length > 0)
                {
                    foreach (var footerAnchor in _footerAnchors)
                    {

                        viewContext.Writer.Write(footerAnchor);

                    }
                }
                viewContext.Writer.Write("</div>");

            }
        }

    }
}