namespace CreaDev.Framework.Web.Mvc.Html.Helpers
{
    public static class ViewPathByConventionHelper
    {
        private static string _path = "~/Views/{0}/{1}.cshtml";
        private const string FORM = "_Form";
        private const string LIST = "_List";
        private const string SEARCH_FORM = "_SearchForm";
        public static string Form(object theModel)
        {
            string className = theModel.GetType().Name;
            return Form(className);
        }
        public static string Form(string className)
        {
            return string.Format(_path, className, FORM);
        }

        public static string GetFullPath(string className, PartialExtension.PartialType partialType)
        {
            string fullPath = string.Empty;
            switch (partialType)
            {
                    case PartialExtension.PartialType.Form:
                    fullPath =Form(className);
                    break;
            }
            return fullPath;
        }
    }
}