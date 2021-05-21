using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Models
{
    
    public class RangeModel<TType>
    {

        public TType From { get; set; }
        public TType To { get; set; }

        public RangeModel()
        {
            
        }

        public RangeModel(TType from, TType to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
