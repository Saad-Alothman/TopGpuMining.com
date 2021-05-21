using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    public class FiatCurrency : GmiEntityBase
    {
        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public string Name { get; set; }
        [Display(Name = "Code")]
        public string Code { get; set; }
        
        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]
        public string Description { get; set; }
        
        public double ExchangeRateUSD { get; set; }

        public override void Update(object objectWithNewData)
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