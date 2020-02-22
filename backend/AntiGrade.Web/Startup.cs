using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessIntelligence.Core.Middleware;
using BusinessIntelligence.Web.Middleware;
using DigitalSkynet.DotnetCore.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AntiGrade.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc((options)=>{
                options.EnableEndpointRouting = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                    .WithOrigins(_corsOrigins.SelectMany(origin => new []
                    {
                        $"http://{origin}",
                        $"https://{origin}"
                    }).ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });

            services.SetupAuthorization(Configuration);
            services.AddSwaggerDocumentation();
            services.AddHttpContextAccessor();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseGlobalExceptionHandler();

            app.UseSwaggerDocumentation();
            app.UseAuthentication();
            app.UseHealthChecks("/health");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }

        private readonly string[] _corsOrigins = {
            "localhost:5001",
            "localhost:4200"
        };
    }
}
