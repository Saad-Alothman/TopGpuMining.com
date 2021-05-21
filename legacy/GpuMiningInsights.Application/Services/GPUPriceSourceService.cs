using System;
using System.Collections.Generic;
using System.Linq;
using GpuMiningInsights.Domain.Models;
using REME.Persistance;

namespace GpuMiningInsights.Application.Services
{
    public class GpuPriceSourceService : GmiServiceBase<GPUPriceSource, GpuPriceSourceService>
    {
        public GpuPriceSourceService()
        {
            Includes.Add(nameof(GPUPriceSource.Gpu));
            Includes.Add(nameof(GPUPriceSource.PriceSource));
        }

        public void AddForAll()
        {
            var allGpus = Repository.GetAll<Gpu>();
            var allPriceSources = Repository.GetAll<PriceSource>();
            var allGpuPiceSources = Repository.GetAll<GPUPriceSource>();
            List<GPUPriceSource> newGpuPriceSources = new List<GPUPriceSource>();

            foreach (var gpu in allGpus)
            {
                foreach (var piceSource in allPriceSources)
                {
                    bool exist = allGpuPiceSources.FirstOrDefault(p => p.GpuId == gpu.Id && p.PriceSourceId == piceSource.Id) != null;
                    if (!exist)
                    {
                        newGpuPriceSources.Add(new GPUPriceSource()
                        {
                            GpuId = gpu.Id,
                            PriceSourceId = piceSource.Id
                        });
                    }
                }

            }
            if (newGpuPriceSources.Any())
            {
                Repository.Create(newGpuPriceSources);
            }
        }
    }
}