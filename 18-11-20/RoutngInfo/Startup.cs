using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Text;

/**
 *  Задание 
 *  Задатьследующиепутимаршрутизации: 
 *  Library, /Library/Books, /Library/Profile. 
 *  Запрос, отправленный по адресу Library, должен возвращать текст приветствия.
 *  
 *  Путь /Library/Books должен выводить список книг,
 *  записанный в виде файла конфигурации любого типа на выбор учащегося. 
 *  
 *  Путь /Library/Profile должен принимать в качестве  необязательного параметра id,
 *  где, в  соответствии с введенным значением 
 *  (маршрут  должен  принимать  только  целочисленные  значения  от  0  до  5) 
 *  будет  в  экран  браузера выведена информация о пользователе библиотеки под определенным id
 *  (информация должна быть записана  в  виде  файла  конфигурации  любого  формата).  
 *  В  случае,  если  пользователь  не  ввел необязательный параметр,
 *  должна выводится информация о всех пользователях
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

            rBuild.MapRoute("{controller}",
                async c =>
                {
                    StringBuilder sb = new StringBuilder(256);
                    string controller = $"{c.GetRouteValue("controller")}/";

                    sb.Append("<h2>Path:</h2>");
                    sb.Append("</p>");
                    sb.Append(controller);
                    sb.Append("</p>");

                    await c.Response.WriteAsync(sb.ToString());
                });

            rBuild.MapRoute("{controller}/{action}",
                async c =>
                {
                    StringBuilder sb = new StringBuilder(256);
                    string controller = $"{c.GetRouteValue("controller")}/";
                    string action = $"{c.GetRouteValue("action")}/";

                    sb.Append("<h2>Path:</h2>");
                    sb.Append("</p>");
                    sb.Append(controller);
                    sb.Append(action);
                    sb.Append("</p>");

                    await c.Response.WriteAsync(sb.ToString());
                });

            rBuild.MapRoute("{controller}/{action}/{id}", 
                async c =>
                {
                    StringBuilder sb = new StringBuilder(256);

                    string controller = $"{c.GetRouteValue("controller")}/";
                    string action = $"{c.GetRouteValue("action")}/";
                    string id = $"{c.GetRouteValue("id")}/";

                    sb.Append("<h2>Path:</h2>");
                    sb.Append("</p>");
                    sb.Append(controller);
                    sb.Append(action);
                    sb.Append(id);
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
