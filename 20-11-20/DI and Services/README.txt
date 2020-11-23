��������� ������������:

_____________________________________________________________________________________

����� ����������� ������ (������ ����� ���������� Startup):

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
		//��� ��� ����� MessageService ���������� ����������� IMessageSender,
		//������� ���������� ����� �����������, 
		//�� ���� ����� ���������� � ��� �����������:
	        services.AddTransient<IMessageSender, EmailMessageSender>();
        	services.AddTransient<MessageService>();
		/*
		*����� ��� ��������� ������� ����� �������������� ����� MessageService, 
		*��� �������� ������� ����� ������ ����� ���������� ��������� ��������. 
		*��������� �������� �������� ����������� ������ MessageService �� ������� ������������.
		*����� ������� ������� ��� ���� ������������ ������������ � �������� �� � �����������.
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

����� �������� ������ Configure:
	public void Configure(IApplicationBuilder app, IMessageSender sender)
	{
        	app.Run(async (context) =>
        	{
        	    await context.Response.WriteAsync(sender.Send());
        	});
	}
_____________________________________________________________________________________

����� �������� ������ Invoke ���������� middleware:
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
/**
*������� ������������, ������������ � ����� Configure ������ Startup,
*����������� ����� ���������� � ����� Invoke ����������  middleware.
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

����� RequestServices ��������� ������� HttpContext (service locator):
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
�����������:
����� �������� ApplicationServices ������� IApplicationBuilder ������  Startup:
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
