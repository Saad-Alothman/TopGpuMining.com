using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CreaDev.Framework.Core.Helpers
{
    public class DisplayName
    {
        //TODO: Check this later to get displayname of attribute
       
        public static string GetDisplayName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            MemberExpression memberExpression = ((MemberExpression) expression.Body);
            string displaName = memberExpression.Member.Name;

            var attribute = Attribute.GetCustomAttribute(memberExpression.Member, typeof(DisplayAttribute)) as DisplayAttribute;
            
            if (attribute != null)
            {
                displaName = attribute.GetName();
            }
            return displaName;
        }
    }

}