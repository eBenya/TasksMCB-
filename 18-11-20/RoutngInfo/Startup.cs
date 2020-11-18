using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Text;

/**
 *  ������� 
 *  ��������������������������������: 
 *  Library, /Library/Books, /Library/Profile. 
 *  ������, ������������ �� ������ Library, ������ ���������� ����� �����������.
 *  
 *  ���� /Library/Books ������ �������� ������ ����,
 *  ���������� � ���� ����� ������������ ������ ���� �� ����� ���������. 
 *  
 *  ���� /Library/Profile ������ ��������� � ��������  ��������������� ��������� id,
 *  ���, �  ������������ � ��������� ��������� 
 *  (�������  ������  ���������  ������  �������������  ��������  ��  0  ��  5) 
 *  �����  �  �����  �������� �������� ���������� � ������������ ���������� ��� ������������ id
 *  (���������� ������ ���� ��������  �  ����  �����  ������������  ������  �������).  
 *  �  ������,  ����  ������������  ��  ���� �������������� ��������,
 *  ������ ��������� ���������� � ���� �������������
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
