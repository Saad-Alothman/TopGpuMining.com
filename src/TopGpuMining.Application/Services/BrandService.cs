using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class BrandService : GenericService<Brand>
    {
        public BrandService(IGenericRepository repository):base(repository)
        {
        }
       
    }
}
