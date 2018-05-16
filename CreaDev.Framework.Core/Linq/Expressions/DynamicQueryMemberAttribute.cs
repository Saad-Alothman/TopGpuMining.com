using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Linq.Expressions
{
   public class DynamicQueryFieldAttribute: Attribute
    {
       public Type ResourceType { get; set; }
       private string _name;
       public string Name
       {
           get { return GetName(); }
            set { _name = value; }
       }

       private string GetName()
       {
           if (ResourceType != null && !string.IsNullOrWhiteSpace(_name))
           {
                DisplayAttribute da = new DisplayAttribute() {ResourceType = ResourceType,Name = _name };
               return da.GetName();
           }
           return _name;
       }

       public DynamicQueryFieldAttribute()
       {
           
       }
    }
    public class DynamicQueryFieldsAttribute : Attribute
    {
        public string Fields { get; set; }
        public Type ResourceType { get; set; }
        private string _name;
        public string Name
        {
            get { return GetName(); }
            set { _name = value; }
        }

        private string GetName()
        {
            if (ResourceType != null && !string.IsNullOrWhiteSpace(_name))
            {
                DisplayAttribute da = new DisplayAttribute() { ResourceType = ResourceType, Name = _name };
                return da.GetName();
            }
            return _name;
        }

        public DynamicQueryFieldsAttribute()
        {

        }
    }
}
