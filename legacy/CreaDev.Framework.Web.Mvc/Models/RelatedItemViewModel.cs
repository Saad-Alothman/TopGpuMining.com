using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public class RelatedItemViewModel
    {
        public string RelatedItemType { get; set; }
        public string RelatedItemId { get; set; }


        public int? RelatedItemIdInteger
        {
            get
            {

                if (!String.IsNullOrEmpty(this.RelatedItemId))
                {
                    int result = 0;

                    bool success = Int32.TryParse(this.RelatedItemId, out result);

                    if (success)
                        return result;
                }

                return null;
            }
        }
    }
}
