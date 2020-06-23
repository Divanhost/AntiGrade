using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using AntiGrade.Core.Configuration;
using AutoMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace UnitTests
{
    public static class Startup
    {
        private static Lazy<IConfiguration> _configuration = new Lazy<IConfiguration>(() =>
             new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.local.json", optional: true)
            .Build(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static IConfiguration Configuration => _configuration.Value;

        public static void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton(Configuration);

            services.AddDataProtection();

            Dependencies.RegisterDependenciesForTesting(services);
                       
        }

        // private static void OverrideDependencies(IServiceCollection services)
        // {
        // 	services.RemoveAll<ApplicationContext>();
        // 	services.AddDbContext<ApplicationContext>(options =>
        //              options.UseSqlite("Data Source=:memory:"));
        // }
    }
}