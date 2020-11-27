using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 3 
 *Создайте приложение по шаблону Console Application. 
 *Возьмите предыдущий пример (Задание 2),
 *не убирая/не изменяя контекст синхронизации выполните продолжение оператора await
 *в контекстерабочего потокапула потоков.
 **/


namespace ConsoleTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"ContextSTART: {SynchronizationContext.Current}");
            SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());
            MainAsync();

            Console.ReadKey();
        }

        static async void MainAsync()
        {
            //var synchrCont = new ConsoleSynchronizationContext();
            PrintInfo("Main Start", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            await FactorialAsync(7);
            //synchrCont.Post(await (a) => FactorialAsync(7), null);
            Console.WriteLine($"ContextEND: {SynchronizationContext.Current}");

            PrintInfo("Main End", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

        }

        private static async Task<int> FactorialAsync(int num)
        {
            return await Task.Run(() =>
            {
                for (int i = num; i > 0; i++)
                {
                    num *= i;
                }
                return num;
            });
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
            Thread.CurrentThread.Name = "NAME_THREAD";

            Program.PrintInfo("Pool", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
        }
    }
}
