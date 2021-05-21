using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.Helpers
{
    public class DateTimeProvider : IModelBinderProvider
    {
        private readonly ILoggerFactory _logger;

        public DateTimeProvider(ILoggerFactory logger)
        {
            _logger = logger;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {

            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType)
            {
                // Look for scrubber attributes
                var underlyingOrModelType = context.Metadata.UnderlyingOrModelType;
                var isDateTime = underlyingOrModelType == typeof(DateTime) || underlyingOrModelType == typeof(DateTime?);

                if (isDateTime)
                    return new DateTimeBinder(underlyingOrModelType, _logger);
            }

            return null;
        }
    }
}
