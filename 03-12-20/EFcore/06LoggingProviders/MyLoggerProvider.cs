using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST
{
    public class MyLoggerProvider : ILoggerProvider
    {
        //Creates and returns a logger object. For create it, use the path to file, that passed throught the constructor.
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        //Manage the reliase of resources.
        public void Dispose() {}

        private class MyLogger : ILogger
        {
            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="TState"></typeparam>
            /// <param name="state"></param>
            /// <returns>Returns an IDisposable object that represents some scope for the logger.</returns>
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            //Indicatade wather the object is available for use.
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            /// <summary>
            /// Makes a log. In this implementation write to the file and console.
            /// </summary>
            /// <typeparam name="TState">The type of object to write.</typeparam>
            /// <param name="logLevel">Level of detail of the current message.</param>
            /// <param name="eventId">Event ID.</param>
            /// <param name="state">Some state object that store the message.</param>
            /// <param name="exception">Information about exception.</param>
            /// <param name="formatter">Formating function, with state and exception parametrs.</param>
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                File.AppendAllText("log.txt", formatter(state, exception));
                Console.WriteLine(formatter(state, exception));
            }
        }
    }


}
