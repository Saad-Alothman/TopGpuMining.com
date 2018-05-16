using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Resources;
using GpuMiningInsights.Core.Resources;

namespace GpuMiningInsights.Core.Exceptions
{
    public class ValidationException : BusinessException
    {
        public List<string> ValidationErrors { get; private set; } = new List<string>();

        public ValidationException(DbEntityValidationException validationException)
        {
            foreach (var item in validationException.EntityValidationErrors)
            {
                foreach (var error in item.ValidationErrors)
                {
                    var entity = item.Entry.Entity;

                    if (error.PropertyName.Contains(".Arabic"))
                    {
                        var displayName = GetDisplayName(entity.GetType(), error.PropertyName.Replace(".Arabic", ""));
                        ValidationErrors.Add(String.Format(Validations.Required, $"{displayName} {Common.Arabic}"));
                    }
                    else if (error.PropertyName.Contains(".English"))
                    {
                        var displayName = GetDisplayName(entity.GetType(), error.PropertyName.Replace(".English", ""));
                        ValidationErrors.Add(String.Format(Validations.Required, $"{displayName} {Common.English}"));
                    }
                    else
                        ValidationErrors.Add($"{error.ErrorMessage}");
                }

            }
        }

        public ValidationException(List<string> errors)
        {
            this.ValidationErrors = errors;
        }


        private string GetDisplayName(Type type, string propertyName)
        {
            var properties = type.GetProperties()
                            .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                            .Select(p => new
                            {
                                PropertyName = p.Name,
                                p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name,
                                DisplayName = p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().GetName()
                            }).ToList();

            return properties.Where(a => a.PropertyName == propertyName).FirstOrDefault()?.DisplayName;


        }
    }
}
