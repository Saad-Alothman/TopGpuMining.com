using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Linq.Expressions
{
   
    public enum Operator
    {
        [Display(ResourceType = typeof(Common),Name = "Equals")]
        Equals,
        [Display(ResourceType = typeof(Common),Name = "GreaterThan")]
        GreaterThan,
        [Display(ResourceType = typeof(Common),Name = "LessThan")]
        LessThan,
        [Display(ResourceType = typeof(Common),Name = "GreaterThanOrEqual")]
        GreaterThanOrEqual,
        [Display(ResourceType = typeof(Common),Name = "LessThanOrEqual")]
        LessThanOrEqual,
        [Display(ResourceType = typeof(Common),Name = "Contains")]
        Contains,
        [Display(ResourceType = typeof(Common),Name = "StartsWith")]
        StartsWith,
        [Display(ResourceType = typeof(Common),Name = "EndsWith")]
        EndsWith
    }
}