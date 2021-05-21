using System;
using System.Collections.Generic;
using System.Linq;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class GpuPriceSourceService : GenericService<GpuPriceSource>
    {
        public GpuPriceSourceService(IGenericRepository repository) : base(repository)
        {
            Includes = new string[]{
                nameof(GpuPriceSource.Gpu),
                nameof(GpuPriceSource.PriceSource)
                };
        }

        public void AddForAll()
        {
            var allGpus = _repository.Get<Gpu>();
            var allPriceSources = _repository.Get<PriceSource>();
            var allGpuPiceSources = _repository.Get<GpuPriceSource>();
            List<GpuPriceSource> newGpuPriceSources = new List<GpuPriceSource>();

            foreach (var gpu in allGpus)
            {
                foreach (var piceSource in allPriceSources)
                {
                    bool exist = allGpuPiceSources.FirstOrDefault(p => p.GpuId == gpu.Id && p.PriceSourceId == piceSource.Id) != null;
                    if (!exist)
                    {
                        newGpuPriceSources.Add(new GpuPriceSource()
                        {
                            GpuId = gpu.Id,
                            PriceSourceId = piceSource.Id
                        });
                    }
                }

            }
            if (newGpuPriceSources.Any())
                _repository.Create<GpuPriceSource>(newGpuPriceSources);
        }
    }
}