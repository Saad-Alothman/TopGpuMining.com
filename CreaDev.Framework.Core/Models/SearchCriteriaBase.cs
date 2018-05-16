using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Models
{
    public class SearchCriteriaBase
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int StartIndex { get { return PageSize * (PageNumber - 1); } }

        public bool IsDescending { get; set; }
        
        public SearchCriteriaBase()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
        }
        public SearchCriteriaBase(int pageSize, int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }


    }
}
