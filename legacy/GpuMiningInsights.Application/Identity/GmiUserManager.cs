using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Persistance;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace GpuMiningInsights.Application.Identity
{
    public class EmployeeInfo
    {
        public const string CalimName = "EmployeeInfo";

        public EmployeeInfo()
        {

        }
        public EmployeeInfo(int? id, string employeeId, int? managerId, int? companyId, int? departmentId,  string companyColor)
        {
            this.Id = id;
            this.EmployeeId = employeeId;
            this.ManagerId = managerId;
            this.CompanyId = companyId;
            this.DepartmentId = departmentId;
            this.CompanyColor = companyColor;
        }


        public int? Id { get; set; }


        public int? DepartmentId { get; set; }

        public int? CompanyId { get; set; }

        public int? ManagerId { get; set; }

        public string EmployeeId { get; set; }
        public string CompanyColor { get; set; }
    }
    public class CHRSUserManager : UserManager<User>
    {
        public override Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            return base.CreateIdentityAsync(user, authenticationType);
        }

        public CHRSUserManager(CHRSUserStore<User> store) : base(store)
        {

        }

        public static CHRSUserManager Create(IdentityFactoryOptions<CHRSUserManager> options, IOwinContext context)
        {

            CHRSUserManager manager = CreateUserManagerDefaultSettings();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("CHRS Identity Data Protection"));
            }

            return manager;
        }

        public static CHRSUserManager Initialize()
        {
            return CreateUserManagerDefaultSettings();
        }

        public static CHRSUserManager Initialize(GmiContext context)
        {
            return CreateUserManagerDefaultSettings(context);
        }

        private static CHRSUserManager CreateUserManagerDefaultSettings()
        {
            var provider = new DpapiDataProtectionProvider("CHRSDataProtection");
            var manager = new CHRSUserManager(new CHRSUserStore<User>(new GmiContext()));



            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Domain.Models.User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Domain.Models.User>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Domain.Models.User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.UserTokenProvider = new DataProtectorTokenProvider<Domain.Models.User>(provider.Create("EmailConfirmationForCHRSRegistered"));

            //manager.EmailService = new EmailService();
            return manager;
        }

        private static CHRSUserManager CreateUserManagerDefaultSettings(GmiContext context)
        {
            var provider = new DpapiDataProtectionProvider("CHRSDataProtection");
            var manager = new CHRSUserManager(new CHRSUserStore<User>(context));



            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Domain.Models.User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };



            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Domain.Models.User>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Domain.Models.User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.UserTokenProvider = new DataProtectorTokenProvider<Domain.Models.User>(provider.Create("EmailConfirmationForCHRSRegistered"));

            //manager.EmailService = new EmailService();
            return manager;
        }

    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            using (SmtpClient client = new SmtpClient())
            {


            }
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
