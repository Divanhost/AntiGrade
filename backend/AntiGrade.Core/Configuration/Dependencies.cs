using AntiGrade.Core.Services.Implementation;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Data.Context;
using AntiGrade.Data.Repositories.Implementation;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AntiGrade.Core.Configuration
{
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.RegisterCommonDependencies();
            AddDbContext(services, configuration);
        }

        public static void RegisterDependenciesForTesting(this IServiceCollection services)
        {
            services.RegisterCommonDependencies();
            services.RegisterTestOnlyDependencies();
           // AddTestDbContext(services);
        }

        private static void RegisterCommonDependencies(this IServiceCollection services)
        {
            RegisterHelpers(services);
            RegisterServices(services);
            AddAutoMapper(services);
        }

        private static void RegisterTestOnlyDependencies(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettingsTest.json")
                .Build();
            services.AddSingleton<IConfiguration>(config);
        }

        private static void RegisterHelpers(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContextAccessor, ContextAccessor>();
            services.AddScoped<UserManager<User>>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer().AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connection, x => x.MigrationsAssembly("AntiGrade.Data")));
        }

        // private static void AddTestDbContext(IServiceCollection services)
        // {
        //     var connectionStringBuilder = new SqliteConnectionStringBuilder
        //     {
        //         DataSource = ":memory:"
        //     };
        //     var connectionString = connectionStringBuilder.ToString();
        //     var connection = new SqliteConnection(connectionString);
        //     connection.Open(); 
        //     services.AddDbContext<AppDbContext>(options =>
        //         options.UseSqlite(connection,
        //             x => x.MigrationsAssembly("Antigrade.Data")), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
        // }
    }
}
