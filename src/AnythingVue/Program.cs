using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Anything
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// The name of the configuration folder, which is relative to HostingEnvironment.ContentRootPath.
        /// Constant is also used in components where IWebHostEnvironment is injected
        /// </summary>
        public const string ConfigurationFolder = "Configuration";

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var configPath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, ConfigurationFolder);
                    config.SetBasePath(configPath)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                });
    }
}
