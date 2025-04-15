using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;
using SmartInventoryBE.Repository;
using SmartInventoryBE.Services;
using SmartInventoryBE.Settings;

namespace SmartInventoryBE
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<SmartInventoryContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    b => b
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
        }
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            service.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
            service.AddTransient(typeof(IProductRepository), typeof(ProductRepository));
            service.AddTransient(typeof(IInventoryRepository), typeof(InventoryRepository));
        }

        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<ITokenService, TokenService>();
            service.AddTransient<ICacheService, CacheService>();
            service.AddTransient<IIoTService, IoTService>();
            service.AddScoped<IWorkContextService, WorkContextService>();
            service.AddHttpContextAccessor();
        }

        public static void AddIdentity(this IServiceCollection services, IConfigurationSection? jwtConfigurationSection)
        {
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<SmartInventoryContext>();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme =
                    opts.DefaultChallengeScheme =
                        opts.DefaultForbidScheme =
                            opts.DefaultScheme =
                                opts.DefaultSignInScheme =
                                    opts.DefaultSignOutScheme =
                                        JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                if (jwtConfigurationSection == null) return;
                opts.IncludeErrorDetails = true;
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfigurationSection.GetValue<string>("Issuer"),
                    // Don't know why audience validation has some errors here, need in-depth investigation
                    ValidateAudience = false,
                    ValidAudience = jwtConfigurationSection.GetValue<string>("Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigurationSection.GetValue<string>("SigningKey") ?? string.Empty))
                };
                opts.UseSecurityTokenValidators = true;
            });
        }

        public static void RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeneralSettings>(configuration.GetSection(GeneralSettings.SectionName));
        }
    }
}
