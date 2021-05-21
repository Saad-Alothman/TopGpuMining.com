using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class PriceSourceService : GenericService<PriceSource>
    {
        public PriceSourceService(IGenericRepository repository) : base(repository)
        {
        }
    }
}
