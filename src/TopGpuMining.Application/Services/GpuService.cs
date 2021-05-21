using System.Collections.Generic;
using System.Linq;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class GpuService : GenericService<Gpu>
    {
        public GpuService(IGenericRepository repository) : base(repository)
        {
            Includes = new[]
            {
                nameof(Gpu.GPUPriceSources),
                $"{nameof(Gpu.GPUPriceSources)}.{nameof(GpuPriceSource.PriceSource)}",
                nameof(Gpu.Model),
            };
        }

        public void Add(List<Gpu> models)
        {
            List<string> asins = models.Select(s => s.Asin).ToList();
            var existing = Search(new SearchCriteria<Gpu>(int.MaxValue, 1)
            {
                FilterExpression = g => asins.Contains(g.Asin)
            }).Result;
            List<Gpu> nonExisting = models.Where(m => existing.All(e => e.Asin != m.Asin)).ToList();
            
            _repository.Create<Gpu>(nonExisting);
        }
    }
}