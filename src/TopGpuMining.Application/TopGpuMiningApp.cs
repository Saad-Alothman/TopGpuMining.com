using TopGpuMining.Application.Identities;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using TopGpuMining.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Application
{
    public class TopGpuMiningApp
    {
        public static void Initialize(IServiceCollection services,IConfiguration configuration)
        {

            services.AddTransient<ITopGpuMiningUserManager<User>, TopGpuMiningUserManager>();
            services.AddTransient<IGenericRepository, GenericRepository>(config => new GenericRepository());

            //domain services
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
