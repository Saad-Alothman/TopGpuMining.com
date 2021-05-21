using System.ComponentModel.DataAnnotations;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Domain.Models
{
    public class FiatCurrency : BaseEntity
    {
        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public string Name { get; set; }
        [Display(Name = "Code")]
        public string Code { get; set; }
        
        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]
        public string Description { get; set; }
        
        public double ExchangeRateUSD { get; set; }

        public void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as FiatCurrency;
            this.Code = updateData.Code;
            this.Description = updateData.Description;

        }
        public void UpdateExchangeRate(double exchangeRate)
        {
            this.ExchangeRateUSD = exchangeRate;

        }
    }
}