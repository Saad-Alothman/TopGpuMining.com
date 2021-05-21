using System;
using System.Collections.Generic;
using CreaDev.Framework.Core.Exceptions;

namespace CreaDev.Framework.Core.Linq.Expressions
{
    public class Filter
    {
        public string PropertyName { get; set; }
        public Operator Operation { get; set; }
        public object Value { get; set; }
        public FilterAggregationType FilterAggregationType { get; set; }



        public enum MacroType
        {
            Today=1,User=2,Project=3,ThisWeek=4
        }
        //public class Macro
        //{
        //    public MacroType MacroType { get; set; }

        //    public static List<Macro> Macros
        //    {
        //        get
        //        {
        //            return new List<Macro>()
        //            {
                        
        //            };
        //        }
        //    }
        //}
        
    }

}