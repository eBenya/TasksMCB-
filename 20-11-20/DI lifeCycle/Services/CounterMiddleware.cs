using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Services
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate next;
        private int i;
        public CounterMiddleware(RequestDelegate _next)
        {
            i = 0;
            next = _next;
        }
        public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService)
        {
            i++;
            /**
             *  Тут в чем прикол, в зависимости от типа регистрации Trincient, Scoped, Singleton -
             * - результаты будут,
             *внезапно, разные! 
             *  В случае Trinsient - Counter и Service, возвращают, преимущественно,
             *разные значения, т.к. объект типа ICounter будет пересоздаваться.
             *  В случае Scouped - Counter и Service, возвращают одинаковые значения,
             *но меняются при каждом новом запросе.
             *  В случае SingleTon - Counter и Service, всегда один объект.
             **/
            await context.Response.WriteAsync($"Request {i};\nObjects:\n\tCounter: {counter.Value};\n\tService: {counterService.Counter.Value}");
        }
    }
}
