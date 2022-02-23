using Anything.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Anything
{
    public class Startup
    {
        
        /// <summary>
        /// The Startup class configures services and the application's request pipeline. 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
            
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();                
            });

            Logger = loggerFactory.CreateLogger<Startup>();
        }

        /// <summary>
        /// Gets the application configuration properties of this application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Get the information about the web hosting environment of this application.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Gets the logger for class <see cref="Startup"/>.
        /// </summary>
        public ILogger<Startup> Logger { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AnythingDb>(options =>
              options.UseSqlite("DataSource=anything.db"));

            var mvcBuilder = services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = true;
                })
                .AddSessionStateTempDataProvider();
#if DEBUG
            // Not to be added in production
            if (WebHostEnvironment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
#endif
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}