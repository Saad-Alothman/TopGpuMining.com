using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Linq.Expressions
{
    public enum FilterAggregationType
    {
        [Display(ResourceType = typeof(Common),Name = "And")]
        And,
        [Display(ResourceType = typeof(Common),Name = "Or")]
        Or
    }
}