using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




/**
 *  Задание 2
 * Создайте небольшой сервис с жизненным циклом Transient.
 * Сервис должен анализировать текущее время на сервере и, в зависимости от результата, 
 * в форме http-ответа возвращать строку «сейчас день» (есливремя между 12 и 16 часами), 
 * «сейчас вечер» (если между 16 и полночью), «сейчас ночь» (от полночи до 4 утра) и, 
 * соответственно, «сейчас утро»(от 4 до 12)
 **/
namespace Services
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITimeService, TimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ITimeService time)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(time.GetTimeOfDay());
                });
            });
        }
    }
}
