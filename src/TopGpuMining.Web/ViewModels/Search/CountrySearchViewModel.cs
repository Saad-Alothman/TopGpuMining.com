using TopGpuMining.Core.Resources;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class CountrySearchViewModel : SearchViewModelBase<Country>
    {
        public string Id { get; set; }

        [Display(Name = nameof(CommonText.Keyword), ResourceType = typeof(CommonText))]
        public string Keyword { get; set; }

        [Display(Name = nameof(CommonText.CountryCode), ResourceType = typeof(CommonText))]
        public string Code { get; set; }

        public string CodeTwo { get; set; }

        public override SearchCriteria<Country> ToSearchModel()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                AddAndFilter(a => a.Id == Id);

                return this;
            }

            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                AddOrFilter(a => a.NameArabic.Contains(Keyword));

                AddOrFilter(a => a.NameEnglish.Contains(Keyword));

                AddOrFilter(a => a.CodeTwo.Contains(Keyword));

                AddOrFilter(a => a.Id.Contains(Keyword));
            }

            if (!string.IsNullOrWhiteSpace(Code))
            {
                var codeUpperCase = Code.ToUpper();

                AddAndFilter(a => a.Id == codeUpperCase);
            }

            if (!string.IsNullOrWhiteSpace(CodeTwo))
            {
                var codeTwoUpperCase = CodeTwo.ToUpper();

                AddAndFilter(a => a.CodeTwo == codeTwoUpperCase);
            }


            return base.ToSearchModel();
        }
    }
}
