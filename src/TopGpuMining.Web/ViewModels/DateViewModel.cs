using TopGpuMining.Core.Resources;
using TopGpuMining.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class DateViewModel
    {
        public DateTime? StartDate { get; set; }

        [GreaterThanDate(PropertyToCompare = nameof(StartDate), ErrorMessageResourceName = nameof(ValidationText.EndDateMustBeGreateThanStartDate), ErrorMessageResourceType = typeof(ValidationText))]
        public DateTime? EndDate { get; set; }


        public DateTime? Year { get; set; }

        public DateTime? Month { get; set; }

        public DateTime? Date { get; set; }

    }
}
