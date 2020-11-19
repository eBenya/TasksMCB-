using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingConstr
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var rBuild = new RouteBuilder(app);

            rBuild.MapGet("{controller:int}/{action:int?}/{id:int?}",
                async c => {
                    await c.Response.WriteAsync("KekPuk");
                });
                                                                      //“ут что-то не так, оно всгда строка и ее нужно сплитить по '/'
            rBuild.MapGet("{controller:int}/{action:int?}/{id:int?}/{*cachall:int}",
                async c => {
                    await c.Response.WriteAsync("KekPuk");
                });

            app.UseRouter(rBuild.Build());

            app.Run(async c => {
                await c.Response.WriteAsync("DefPage");
            });
        }
    }
}
