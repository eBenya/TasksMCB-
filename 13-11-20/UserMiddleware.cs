using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
     Задание:
    Разработайте и подключите в конвейер обработки запросов
    пользовательский компонент Middleware, 
    который будет передавать в виде ответа клиенту тело запроса
*/
namespace UserMidleWare
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var body = context.Request.Body;
            

            StringBuilder sb = new StringBuilder(200);

            sb.Append($"Method: {context.Request.Method}\n");
            sb.Append($"Protocol: {context.Request.Protocol}\n");
            sb.Append($"Path: {context.Request.Path}\n");
            sb.Append($"Scheme: {context.Request.Scheme}\n");

            sb.Append($"Host: {context.Request.Host.Value}\n");
            sb.Append($"ContentType: {context.Request.ContentType}\n");
            sb.Append($"PathBase: {context.Request.PathBase}\n");

            #region Cookies
            sb.Append("Cookies:\n");
            foreach (var item in context.Request.Cookies)
            {
                sb.Append($"\tKey:{item.Key}\n\tValue:{item.Value}\n\n");
            }
            #endregion

            #region RouteValues
            sb.Append("RouteValues:\n");
            foreach (var item in context.Request.RouteValues)
            {
                sb.Append($"\tKey:{item.Key}\n\tValue:{item.Value}\n\n");
            }
            #endregion

            sb.Append($"QueryString.Value: {context.Request.QueryString.Value}\n");
            #region Query
            sb.Append("Query:\n");
            foreach (var item in context.Request.Query)
            {
                sb.Append($"\tKey:{item.Key}\n\tValue:{item.Value}\n\n");
            }
            #endregion

            #region Headers
            sb.Append("Headers:\n");
            foreach (var item in context.Request.Headers)
            {
                sb.Append($"\tKey:{item.Key}\n\tValue:{item.Value}\n\n");
            }
            #endregion

            #region Body
            sb.Append("Body:\n");            

            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body,
                                                    encoding: Encoding.UTF8,
                                                    false,
                                                    leaveOpen: true))
            {
                sb.Append( await reader.ReadToEndAsync());

                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            #endregion

            await context.Response.WriteAsync(sb.ToString());
        }
    }
}
