using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Image.Common.Log;
using Microsoft.Extensions.Logging;
using Image.Server.Middleware;
using Image.Common.Cache;
using Image.Common.Redis;

namespace Image.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //add log provider
            services.AddSingleton<ILog, Logger>();
            services.AddSingleton<ICache, RedisClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseGlobErrorHandlingMiddleware();
            //load nlog config
            NLog.LogManager.LoadConfiguration("Configuration/nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}");
            });
            // This must be excuted after app.usemvc in order to catch glob exception correctly
            app.UseGlobErrorHandlingMiddleware();
        }
    }
}
