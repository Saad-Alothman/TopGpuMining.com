using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Application;
using TopGpuMining.Application.Identities;
using TopGpuMining.Core;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;
using TopGpuMining.Persistance;
using TopGpuMining.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace TopGpuMining.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettings.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentity(services);

            ConfigureAuthentication(services);

            services.AddHttpContextAccessor();

            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddScoped<IViewRender, ViewRender>();

            services.AddCloudscribePagination();

            services.AddSession();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddControllersWithViews(config =>
            {
                config.ModelBinderProviders.Insert(0, new DateTimeProvider(new LoggerFactory()));

            }).AddRazorRuntimeCompilation();

            ConfigureCookiesOptoins(services);
            
            TopGpuMiningApp.Initialize(services, Configuration);

            ServiceLocator.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TopGpuMiningDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.EnsureSeeding();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            ConfigureMvcWithCulture(app);
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDbContext<TopGpuMiningDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            services.AddIdentity<User, Role>()
               .AddUserManager<TopGpuMiningUserManager>()
               .AddErrorDescriber<TopGpuMiningIdentityErrorDescriber>()
               .AddClaimsPrincipalFactory<TopGpuMiningClaimsPrincipalFactory>()
               .AddRoleStore<TopGpuMiningRoleStore>()
               .AddUserStore<TopGpuMiningUserStore>()
               .AddSignInManager<TopGpuMiningSignInManager>()
               .AddDefaultTokenProviders();


            services.AddScoped<ITopGpuMiningUserManager<User>>(config => config.GetService<TopGpuMiningUserManager>());
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication()
                            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                            {
                                config.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = false,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = AuthHelper.Issuer,
                                    ValidAudience = AuthHelper.Audience,
                                    IssuerSigningKey = new SymmetricSecurityKey(
                                                        Encoding.UTF8.GetBytes(AuthHelper.Key))
                                };
                            });

            services.AddAuthorization();
        }

        private void ConfigureMvcWithCulture(IApplicationBuilder app)
        {
            var defaultLang = "en";

            var options = GetRequestLocalizationOptions(defaultLang);

            app.UseRequestLocalization(options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture}/{controller=Home}/{action=Index}/{id?}",
                    defaults: new { culture = defaultLang });
            });
        }

        private void ConfigureCookiesOptoins(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(config =>
            {
                config.AccessDeniedPath = GetAccessDeniedPath();

                config.LoginPath = "/en/account/login";

                config.Events.OnRedirectToAccessDenied = context =>
                {
                    var url = context.RedirectUri;

                    url = GetUrlBasedOnLanguage(url);

                    context.Response.Redirect(url);

                    return Task.CompletedTask;
                };

                config.Events.OnRedirectToLogin = context =>
                {
                    var url = context.RedirectUri;

                    url = GetUrlBasedOnLanguage(url);

                    context.Response.Redirect(url);

                    return Task.CompletedTask;
                };
            });
        }

        private string GetUrlBasedOnLanguage(string url)
        {
            if (Language.IsEnglish)
            {
                if (url.Contains("/ar/"))
                {
                    url = url.Replace("/ar/", "/en/");
                }
            }
            else
            {
                url = url.Replace("/en/", "/ar/");
            }

            return url;
        }

        private RequestLocalizationOptions GetRequestLocalizationOptions(string defaultLang)
        {
            var arSACulture = new CultureInfo("ar")
            {
                DateTimeFormat = new CultureInfo("en").DateTimeFormat
            };

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                arSACulture
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultLang),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            options.RequestCultureProviders.Insert(0, requestProvider);
            return options;
        }

        private PathString GetAccessDeniedPath()
        {
            if (Language.IsEnglish)
            {
                return new PathString("/en/account/accessdenied");
            }
            else
            {
                return new PathString("/ar/account/accessdenied");
            }
        }
    }
}
