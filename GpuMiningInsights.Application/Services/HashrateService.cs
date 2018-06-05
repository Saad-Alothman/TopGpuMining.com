using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public class HashrateService : GmiServiceBase<Hashrate, HashrateService>
    {
        public HashrateService()
        {
            Includes.Add(nameof(Hashrate.Algorithm));
            Includes.Add(nameof(Hashrate.Model));
        }
    }
}
