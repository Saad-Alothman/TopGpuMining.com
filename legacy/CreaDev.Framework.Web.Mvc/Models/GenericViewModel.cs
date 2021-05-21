namespace CreaDev.Framework.Web.Mvc.Models
{
    public class GenericViewModel<TEntity> where TEntity : class,new ()
    {
        /*
            redirect
            Related item type
            related item id
            isReturnList if listreturn list of total max of 20 with pagination
        */

        public ViewDisplayMode ViewDisplayMode { get; set; }

        public TEntity Item { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsRedirect => !string.IsNullOrWhiteSpace(RedirectUrl);
        public string UpdateTargetId { get; set; }
        public string AlertSelector { get; set; }
        public RelatedItemViewModel RelatedItemViewModel { get; set; }

        public bool IsReturnList { get; set; }

        public bool IsReturnModelOnly { get; set; }


        public GenericViewModel()
        {
            this.ViewDisplayMode = ViewDisplayMode.EnabledNoButtons;
            this.RelatedItemViewModel = new RelatedItemViewModel();
            //this.Item = new TEntity();
        }

        public GenericViewModel(TEntity item):this()
        {
            this.Item = item;
        }
        public GenericViewModel(TEntity item,string relatedItemType,string relatedItemId) 
        {
            this.Item = item;
            this.RelatedItemViewModel = new RelatedItemViewModel() {RelatedItemType = relatedItemType,RelatedItemId = relatedItemId};
        }
    }
}
