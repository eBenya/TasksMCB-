using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.Text;

/**
 *  Çàäàíèå 
 *  Çàäàòüñëåäóþùèåïóòèìàðøðóòèçàöèè: 
 *  Library, /Library/Books, /Library/Profile. 
 *  Çàïðîñ, îòïðàâëåííûé ïî àäðåñó Library, äîëæåí âîçâðàùàòü òåêñò ïðèâåòñòâèÿ.
 *  
 *  Ïóòü /Library/Books äîëæåí âûâîäèòü ñïèñîê êíèã,
 *  çàïèñàííûé â âèäå ôàéëà êîíôèãóðàöèè ëþáîãî òèïà íà âûáîð ó÷àùåãîñÿ. 
 *  
 *  Ïóòü /Library/Profile äîëæåí ïðèíèìàòü â êà÷åñòâå  íåîáÿçàòåëüíîãî ïàðàìåòðà id,
 *  ãäå, â  ñîîòâåòñòâèè ñ ââåäåííûì çíà÷åíèåì 
 *  (ìàðøðóò  äîëæåí  ïðèíèìàòü  òîëüêî  öåëî÷èñëåííûå  çíà÷åíèÿ  îò  0  äî  5) 
 *  áóäåò  â  ýêðàí  áðàóçåðà âûâåäåíà èíôîðìàöèÿ î ïîëüçîâàòåëå áèáëèîòåêè ïîä îïðåäåëåííûì id
 *  (èíôîðìàöèÿ äîëæíà áûòü çàïèñàíà  â  âèäå  ôàéëà  êîíôèãóðàöèè  ëþáîãî  ôîðìàòà).  
 *  Â  ñëó÷àå,  åñëè  ïîëüçîâàòåëü  íå  ââåë íåîáÿçàòåëüíûé ïàðàìåòð,
 *  äîëæíà âûâîäèòñÿ èíôîðìàöèÿ î âñåõ ïîëüçîâàòåëÿõ
 **/
namespace Services
{
    public class Startup
    {
        IConfiguration Conf { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath($"{env.ContentRootPath}\\Configs");

            builder.AddJsonFile("BooksConf.json");
            builder.AddJsonFile("UsersConf.json");

            Conf = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var rBuid = new RouteBuilder(app);

            rBuid.MapRoute("Library",
                async cont => {
                    StringBuilder sb = new StringBuilder(256);

                    sb.Append("<h2>Hi man!</h2>");
                    Editor.AddReffs(sb);

                    await cont.Response.WriteAsync(sb.ToString());
                });

            rBuid.MapRoute("Library/Books",
                async cont => {
                    var myArr = Conf.GetSection("Books");

                    StringBuilder sb = new StringBuilder(256);

                    sb.Append("<h1>Books list:</h1>");
                    sb.Append("<table>");
                    sb.Append("<tr><th>Author</th><th>Name</th>");
                    foreach (var item in myArr.GetChildren())
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td>{item.GetValue<string>("Author")}</td>");
                        sb.Append($"<td>{item.GetValue<string>("Name")}</td>");
                        sb.Append("</tr>");                        
                    }
                    sb.Append("</table>");

                    //Editor.AddReffs(sb);
                    await cont.Response.WriteAsync(sb.ToString());
                });

            rBuid.MapRoute("Library/Profile",
                async cont => {

                    var myArr = Conf.GetSection("Users");
                    StringBuilder sb = new StringBuilder(256);

                    sb.Append("<h1>Users list:</h1>");
                    sb.Append("<table>");
                    sb.Append("<tr><th>id</th><th>Name</th>");
                    foreach (var item in myArr.GetChildren())
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td><a href=\"\\Library\\Profile\\{item.GetValue<int>("id")}\">{ item.GetValue<int>("id")}</a></td>");
                        sb.Append($"<td><a href=\"\\Library\\Profile\\{item.GetValue<int>("id")}\">{item.GetValue<string>("Name")}</a></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");


                    //Editor.AddReffs(sb);
                    await cont.Response.WriteAsync(sb.ToString());
                });

            rBuid.MapRoute("Library/Profile/{id}",
                async cont =>
                {
                    StringBuilder sb = new StringBuilder(256);
                    if (int.TryParse(cont.GetRouteValue("id").ToString(), out int id))
                    {
                        var myArr = Conf.GetSection("Users");
                        
                        string name = string.Empty;
                        foreach (var item in myArr.GetChildren())
                        {
                            if (id == item.GetValue<int>("id"))
                            {
                                name = item.GetValue<string>("Name");
                            }
                        }
                        if (name == string.Empty)
                        {
                            sb.Append("<h2>This man is no such.</h2>");
                        }
                        else
                        {
                            sb.Append("<h1>User:</h1>");
                            sb.Append("<table>");
                            sb.Append("<tr><th>id</th><th>Name</th>");
                            sb.Append($"<tr><td>{id}</td><td>{name}</td></tr>");
                            sb.Append("</table>");
                        }
                    }
                    else
                    {
                        sb.Append("<h2>Error</h2>");
                    }

                    Editor.AddReffs(sb);
                    await cont.Response.WriteAsync(sb.ToString());

                });

            app.UseRouter(rBuid.Build());


            app.Run(async cont =>
            {
                StringBuilder sb = new StringBuilder(256);

                sb.Append("<h2>DeffaultPage</h2>");
                Editor.AddReffs(sb);

                await cont.Response.WriteAsync(sb.ToString());
            });

        }
    }

    static class Editor
    {
        //https:\\localhost:44306    /Library/Books
        public static StringBuilder AddReffs(StringBuilder sb)
        {
            sb.Append("<p><a href=\"\\Library\">Home</a></p>");
            sb.Append("<p><a href=\"\\Library\\Profile\">Users</a></p>");            
            sb.Append("<p><a href=\"\\Library\\Books\">Books</a></p>");

            return sb;
        }
    }
}
