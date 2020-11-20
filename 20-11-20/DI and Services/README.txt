Внедрение зависимостей:

_____________________________________________________________________________________

Через конструктор класса (Нельзя через коструктор Startup):

	public class MessageService
	{
	    IMessageSender _sender;
	    public MessageService(IMessageSender sender)
	    {
	        _sender = sender;
	    }
	    public string Send()
	    {
	        return _sender.Send();
	    }
	}

	public class Startup
	{
	    public void ConfigureServices(IServiceCollection services)
	    {
		//так как класс MessageService использует зависимость IMessageSender,
		//которая передается через конструктор, 
		//то надо также установить и эту зависимость:
	        services.AddTransient<IMessageSender, EmailMessageSender>();
        	services.AddTransient<MessageService>();
		/*
		*Когда при обработке запроса будет использоваться класс MessageService, 
		*для создания объекта этого класса будет вызываться провайдер сервисов. 
		*Провайдер сервисов проверят конструктор класса MessageService на наличие зависимостей.
		*Затем создает объекты для всех используемых зависимостей и передает их в конструктор.
		*/
	    }
	 
	    public void Configure(IApplicationBuilder app, MessageService messageService)
	    {
	        app.Run(async (context) =>
        	{
	            await context.Response.WriteAsync(messageService.Send());
        	});
	    }
	}
_____________________________________________________________________________________

Через параметр метода Configure:
	public void Configure(IApplicationBuilder app, IMessageSender sender)
	{
        	app.Run(async (context) =>
        	{
        	    await context.Response.WriteAsync(sender.Send());
        	});
	}
_____________________________________________________________________________________

Через параметр метода Invoke компонента middleware:
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
/**
*Подобно зависимостям, передающимся в метод Configure класса Startup,
*зависимости можно передавать в метод Invoke компонента  middleware.
**/
	public class MessageMiddleware
	{
 		private readonly RequestDelegate _next;
 
  	 	public MessageMiddleware(RequestDelegate next)
   	 	{
   	 	    this._next = next;
   	 	}
 
   	 	public async Task InvokeAsync(HttpContext context, IMessageSender messageSender)
   	 	{
    	 	   context.Response.ContentType = "text/html;charset=utf-8";
   	 	   await context.Response.WriteAsync(messageSender.Send());
    		}
	}

	public class Startup
	{
  		public void ConfigureServices(IServiceCollection services)
   		{
     		   services.AddTransient<IMessageSender, EmailMessageSender>();
    		}
    		public void Configure(IApplicationBuilder app)
   		{
    		    app.UseMiddleware<MessageMiddleware>();
    		}
	}

_____________________________________________________________________________________

Через RequestServices контекста запроса HttpContext (service locator):
	public class Startup
	{
	    public void ConfigureServices(IServiceCollection services)
	    {
        	services.AddTransient<IMessageSender, EmailMessageSender>();
	    }
	    public void Configure(IApplicationBuilder app)
	    {
        	app.Run(async (context) =>
	        {
        	    IMessageSender messageSender = context.RequestServices.GetService<IMessageSender>();
	            context.Response.ContentType = "text/html;charset=utf-8";
        	    await context.Response.WriteAsync(messageSender.Send());
	        });
	    }
	}
_____________________________________________________________________________________
Аналогичный:
Через свойство ApplicationServices объекта IApplicationBuilder класса  Startup:
	public void Configure(IApplicationBuilder app)
	{
 	   app.Run(async (context) =>
  	  {
   	     IMessageSender messageSender = app.ApplicationServices.GetService<IMessageSender>();
   	     context.Response.ContentType = "text/html;charset=utf-8";
    	    await context.Response.WriteAsync(messageSender.Send());
   	 });
	}
_____________________________________________________________________________________
