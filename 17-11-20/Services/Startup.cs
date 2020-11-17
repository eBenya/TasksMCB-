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
 *  ������� 2
 * �������� ��������� ������ � ��������� ������ Transient.
 * ������ ������ ������������� ������� ����� �� ������� �, � ����������� �� ����������, 
 * � ����� http-������ ���������� ������ ������� ����� (��������� ����� 12 � 16 ������), 
 * ������� ����� (���� ����� 16 � ��������), ������� ����� (�� ������� �� 4 ����) �, 
 * ��������������, ������� ����(�� 4 �� 12)
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
