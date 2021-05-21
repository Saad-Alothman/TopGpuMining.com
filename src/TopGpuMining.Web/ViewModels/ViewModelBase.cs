using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class ViewModelBase<TModel> where TModel : class, new()
    {
        public virtual TModel ToModel()
        {
            return new TModel();
        }
    }
}
