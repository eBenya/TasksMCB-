using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text;
using System.Diagnostics.CodeAnalysis;



/**
 * Задание 
 * Подключите к серверу три типа файлов конфигурации: xml, json.
 * В каждом из них будет записана информация о компаниях Microsoft, Appleи Google. 
 * В качестве обязательного параметра должно быть количество сотрудников данных компаний.
 * Создайте сервис, который будет анализировать количество сотрудников, 
 * записанное в упомянутых выше файлах конфигурации, и выводить название компании, 
 * у которой этот показатель выше
 **/


namespace Configure
{
    public class Startup
    {
        IConfiguration Conf { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath($"{env.ContentRootPath}\\root");

            //For XML file
            //builder.AddXmlFile("Company.xml");

            //For JSON file
            builder.AddJsonFile("Company.json");

            Conf = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            #region XML
            ////Print Google
            //app.Run(async (c) =>
            //{
            //    var myArray = Conf.GetSection("Company");
            //    Dictionary<string, int> d = new Dictionary<string, int>();
            //    foreach (var item in myArray.GetChildren())
            //    {
            //        d.Add(item.GetValue<string>("Name"), item.GetValue<int>("Employees"));
            //    }
            //    string n = d.FirstOrDefault(x => x.Value == d.Values.Max()).Key;
            //    await c.Response.WriteAsync($"{n}");
            //});
            #endregion

            #region JSON
            //Print Microsoft
            app.Run(async (c) =>
            {
                var myArray = Conf.GetSection("Company");
                Dictionary<string, int> d = new Dictionary<string, int>();
                foreach (var item in myArray.GetChildren())
                {
                    d.Add(item.GetValue<string>("Name"), item.GetValue<int>("Employees"));
                }
                string n = d.FirstOrDefault(x => x.Value == d.Values.Max()).Key;
                await c.Response.WriteAsync($"{n}");
            });
            #endregion
        }
    }



    class Comp
    {
        string Name { get; set; }
        int Employees { get; set; }
    }
}
