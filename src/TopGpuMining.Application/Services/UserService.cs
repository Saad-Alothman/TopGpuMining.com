using TopGpuMining.Application.Identities;
using TopGpuMining.Core;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using TopGpuMining.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Application.Services
{
    public class UserService : GenericService<User> ,IUserService
    {
        public UserService(IGenericRepository repository) : base(repository)
        {
            Includes = new[]
            {
                nameof(User.Roles)
            };
        }

        public async Task<User> AddAsync(User entity, string password)
        {
            var userManager = GetUserManager();

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            var result = await userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(a => a.Description).ToList());

            return entity;
        }

        public override void Delete(string id)
        {
            var userManager = GetUserManager();

            var account = userManager.FindByIdAsync(id).GetAwaiter().GetResult();

            var result = userManager.DeleteAsync(account).GetAwaiter().GetResult();

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(a => a.Description).ToList());
        }

        public User Save(User entity,string password = null)
        {
            var userManager = GetUserManager();

            var user = userManager.FindByIdAsync(entity.Id).GetAwaiter().GetResult();

            user = user.Update(entity);

            RemoveUserRoles(user, userManager);
            
            var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(p => p.Description).ToList());
            
            return entity;
        }

        public List<Role> GetRoles() => _repository.Get<Role>().ToList();

        private void RemoveUserRoles(User user, TopGpuMiningUserManager userManager)
        {
            var exisitRoles = userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            userManager.RemoveFromRolesAsync(user, exisitRoles).GetAwaiter().GetResult();
        }

        private TopGpuMiningUserManager GetUserManager()
        {
            return ServiceLocator.Current.GetService<TopGpuMiningUserManager>();
        }


    }
}
