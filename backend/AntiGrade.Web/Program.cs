using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AntiGrade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext,config)=>
            {
                config.AddJsonFile("appsettings.json");
                config.AddJsonFile("appsettings.{Environment}.json", optional : true);
                config.AddJsonFile("appsettings.local.json", optional : true);
            })
            .UseStartup<Startup>();
    }
}
