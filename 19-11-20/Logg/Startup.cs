using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            logger.Log(LogLevel.Debug, "Mess");

            app.Run(async c =>
            {
                logger.LogInformation($"Processing request {c.Request.Path}");
                await c.Response.WriteAsync("Some");
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "Default", template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }

    }
}
