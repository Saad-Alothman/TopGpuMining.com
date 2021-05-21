using TopGpuMining.Core;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;
using TopGpuMining.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Application.Identities
{
    public class TopGpuMiningUserManager : UserManager<User>, ITopGpuMiningUserManager<User>
    {
        public TopGpuMiningUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
          IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
          IEnumerable<IPasswordValidator<User>> passwordValidators,
          ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
          IServiceProvider services, ILogger<UserManager<User>> logger)
           : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            this.Options = GetDefaultOptions();
        }

        public static TopGpuMiningUserManager Create()
        {
            TopGpuMiningDbContext context = new TopGpuMiningDbContext();

            IUserStore<User> userStore = new UserStore<User, Role, TopGpuMiningDbContext>(context);

            IdentityOptions options = GetDefaultOptions();

            IOptions<IdentityOptions> optionResult = Microsoft.Extensions.Options.Options.Create(options);

            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            TopGpuMiningIdentityErrorDescriber errorDescriber = new TopGpuMiningIdentityErrorDescriber();

            List<UserValidator<User>> validators = new List<UserValidator<User>>();
            UserValidator<User> validator = new UserValidator<User>(errorDescriber);
            validators.Add(validator);

            List<PasswordValidator<User>> passwordValidators = new List<PasswordValidator<User>>();
            PasswordValidator<User> passwordValidator = new PasswordValidator<User>(errorDescriber);
            passwordValidators.Add(passwordValidator);

            UpperInvariantLookupNormalizer normalizer = new UpperInvariantLookupNormalizer();
            
            var logger = AppLogger.LoggerFactory?.CreateLogger<TopGpuMiningUserManager>();
            
            return new TopGpuMiningUserManager(userStore, optionResult, passwordHasher, validators, passwordValidators, normalizer, errorDescriber, null, logger);
        }

        public static IdentityOptions GetDefaultOptions()
        {
            IdentityOptions options = new IdentityOptions()
            {

                SignIn = new SignInOptions()
                {

                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false,
                },
                User = new UserOptions()
                {
                    RequireUniqueEmail = true,
                },
                Lockout = new LockoutOptions()
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15),
                    AllowedForNewUsers = false,
                    MaxFailedAccessAttempts = 10,
                },
                Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 4,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false

                }
            };

            return options;
        }
    }
}
