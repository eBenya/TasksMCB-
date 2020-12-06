using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 4
 *Создайте  приложение  по  шаблону  Console  Application.
 *Создайте  контекст  синхронизации, который  сможет  обрабатывать  ошибки,
 *возникшие  в  асинхронных  методах  с  возвращаемым типом void.
 *Установите созданный контекст синхронизации
 *и проверьте вызовом асинхронного метода с типом void,
 *обрабатывается ли ваша ошибка в контексте. 
 *Уберите установку контекста синхронизации. 
 *Сделайте выводы на счет использования асинхронных методов с типом void.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"ContextSTART: {SynchronizationContext.Current}");
            PrintInfo("Main Start", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            //SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());

            FactorialAsync(7);

            Console.WriteLine($"ContextEND: {SynchronizationContext.Current}");
            PrintInfo("Main End", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);
        }

        private static async void FactorialAsync(int num)
        {
            PrintInfo("До await в методе", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Run(() => Factorial(7));

            //return res;
        }

        private static void Factorial(int num)
        {
            for (int i = num; i > 0; i--)
            {
                num *= i;
            }
            Console.WriteLine($"Factorial = {num}");
            return;
        }

        public static void PrintInfo(string text, int ManagedID, string threadName, bool isThreadPoolThread)
        {
            Console.WriteLine($"{text} ID:{ManagedID}; Name:{threadName}; From thread pool - {isThreadPoolThread}");
        }
    }

    class ConsoleSynchronizationContext: SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            try
            {
                throw new Exception("Exception in ConsoleSenhronizationContext");
                d.Invoke(state);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error!!!!!!!! {e.Message}");
            }
        }
    }
}
