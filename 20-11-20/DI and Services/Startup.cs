using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

using testApp.Services;

namespace testApp
{
    public class Startup
    {
        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;   //Этот парраметр нельзя получить через конструктор класса Startup
            services.AddRouting();
            services.AddTransient<IMessageService, SmsMessService>();
            services.AddTransient<IMessageService, EmailMessService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageService messService)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.Map("/info", Info);

            app.Run(async c =>
            {
                await c.Response.WriteAsync("Mess");//messService.Send());
            });
        }

        private void Info(IApplicationBuilder app)
        {
            app.Run(async c =>
            {
                var sb = new StringBuilder(256);
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table cellspacing=\"2\" border=\"1\" cellpadding=\"5\">");
                sb.Append("<tr><th>Type</th><th>Life Time</th><th>Realization</th></tr>");
                foreach (var item in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{item.ServiceType.FullName}</td>");
                    sb.Append($"<td>{item.Lifetime}</td>");
                    sb.Append($"<td>{item.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                await c.Response.WriteAsync(sb.ToString());
            });
        }
    }
}
