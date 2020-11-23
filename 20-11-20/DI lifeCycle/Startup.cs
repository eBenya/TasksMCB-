using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

using testApp.Services;

namespace testApp
{
    public class Startup
    {
        private IServiceCollection services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;

            //services.AddTransient<ICounter, RandomCounter>();
            //services.AddTransient<CounterService>();

            //services.AddScoped<ICounter, RandomCounter>();
            //services.AddScoped<CounterService>();

            services.AddSingleton<ICounter, RandomCounter>();
            services.AddSingleton<CounterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<CounterMiddleware>();
        }
    }
}
