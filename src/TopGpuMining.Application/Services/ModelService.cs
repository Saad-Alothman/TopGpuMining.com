using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class ModelService : GenericService<Model>
    {
        public ModelService(IGenericRepository repository) : base(repository)
        {
        }
    }
}