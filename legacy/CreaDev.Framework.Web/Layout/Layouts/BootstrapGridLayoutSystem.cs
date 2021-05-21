namespace CreaDev.Framework.Web.Layout.Layouts
{
    public  class BootstrapGridLayoutSystem : GridLayoutSystem
    {

        public string FormGroup { get { return "form-group"; } }

        private static BootstrapGridLayoutSystem _instatnce;

        public static BootstrapGridLayoutSystem Instatnce
        {
            get
            {
                if (_instatnce == null)
                {
                    _instatnce = new BootstrapGridLayoutSystem();
                }
                return _instatnce;
            }
        }



        private BootstrapGridLayoutSystem()
        {
            
        }


        public string MediumColumn(int width)
        {
            return Column(width, GridLayoutSystemColumnSize.Medium);
        }
        public string MediumOffset(int width)
        {
            return Offset(width, GridLayoutSystemColumnSize.Medium);
        }

    }
}