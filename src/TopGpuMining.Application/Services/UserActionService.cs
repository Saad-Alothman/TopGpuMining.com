using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class UserActionService : GenericService<UserAction>
    {

        public UserActionService(IGenericRepository repository) : base(repository)
        {
        }
    }
}