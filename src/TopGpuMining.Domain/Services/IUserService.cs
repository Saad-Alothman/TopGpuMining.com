using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Domain.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> AddAsync(User entity, string password);
        List<Role> GetRoles();
        User Save(User entity, string password = null);

    }
}
