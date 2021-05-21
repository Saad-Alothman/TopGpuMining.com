﻿using TopGpuMining.Domain.Models;

namespace TopGpuMining.Domain.Services
{
    public interface IAlgorithmService : IGenericService<Algorithm>
    {
        Algorithm AddOrUpdate(Algorithm model);

    }
}