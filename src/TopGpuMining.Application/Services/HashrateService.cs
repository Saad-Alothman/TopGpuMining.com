using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class HashrateService : GenericService<Hashrate>
    {
        public HashrateService(IGenericRepository repository):base(repository)
        {
            Includes = new[] {
                nameof(Hashrate.Algorithm),
                nameof(Hashrate.Model)
                };
        }
    }
}
