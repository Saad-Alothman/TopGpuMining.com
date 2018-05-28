using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public class AlgorithmService : GmiServiceBase<Algorithm, AlgorithmService>, IAlgorithmService
    {
        public Algorithm AddOrUpdate(Algorithm model)
        {
            var algoInDB=Search(new SearchCriteria<Algorithm>(a => a.Name.English.ToLower() == model.Name.English.Trim().ToLower())).Result.FirstOrDefault();
            if (algoInDB != null)
                return algoInDB;
            Add(model);
            algoInDB=Search(new SearchCriteria<Algorithm>(a => a.Name.English.ToLower() == model.Name.English.Trim().ToLower())).Result.FirstOrDefault();

            return algoInDB;
        }
    }
}
