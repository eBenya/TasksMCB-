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
        static Program()
        {
            SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"ContextSTART: {SynchronizationContext.Current}");
            MainAsync();

            Console.ReadKey();
        }

        static async Task MainAsync()
        {
            PrintInfo("Main Start", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            FactorialAsync(7);

            Console.WriteLine($"ContextEND: {SynchronizationContext.Current}");
            PrintInfo("Main End", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);
        }

        private static async Task<int> FactorialAsync(int num)
        {
            PrintInfo("До await в методе", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            int res = await Task.Run(() => Factorial(7));

            return res;
        }

        private static int Factorial(int num)
        {
            for (int i = num; i > 0; i--)
            {
                num *= i;
            }
            return num;
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
            if (Thread.CurrentThread.Name is null)
            {
                Thread.CurrentThread.Name = "NAME_THREAD";
            }

            Program.PrintInfo("Pool", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);

            d.Invoke(state);
        }
    }
}
