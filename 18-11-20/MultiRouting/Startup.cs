using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;

/**
 *  Задание 2
 Задайте путь  маршрутизации,  
 который  принимает  множественный  параметр
 и последовательно выводит в окно браузера
 только те значения сегментов, 
 которые содержат в себе цифры. 
 Если же таких значений нет, или параметр пуст, 
 должна быть выведена ошибка.
 **/
namespace Services
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
        public void Configure(IApplicationBuilder app)
        {
            var rBuild = new RouteBuilder(app);

            rBuild.MapRoute("{controller}/{action}/{id}/{*catchall}",
                async c =>
                {
                    StringBuilder sb = new StringBuilder(256);

                    var catchall = c.GetRouteData();
                    string[] catchall2 = c.GetRouteValue("catchall")?.ToString()?.Split('/');

                    sb.Append("<h2>Path:</h2>");
                    sb.Append("</p>~/");
                    if (catchall2 != null && catchall2.Length > 0)
                    {
                        foreach (var item in catchall2)
                        {
                            if (int.TryParse(item, out int n))
                            {
                                sb.Append($"{item}/");
                            }
                            else
                            {
                                sb.Append("Error/");
                            }
                        }
                    }
                    sb.Append("</p>");

                    await c.Response.WriteAsync(sb.ToString());
                });

            app.UseRouter(rBuild.Build());
            app.Run(async cont =>
            {
                await cont.Response.WriteAsync("<h2>DeffaultPage</h2>");
            });

        }
    }
}
