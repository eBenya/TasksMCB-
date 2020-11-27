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
            MainAsync();
            Console.ReadKey();
        }

        static async void MainAsync()
        {
            //var synchrCont = new ConsoleSynchronizationContext();

            Console.WriteLine($"ID:{Thread.CurrentThread.ManagedThreadId}; Name:{Thread.CurrentThread.Name}; From thread pool? - {Thread.CurrentThread.IsThreadPoolThread}");

            FactorialAsync(7);
            //synchrCont.Post(await (a) => FactorialAsync(7), null);

            Console.WriteLine($"ID:{Thread.CurrentThread.ManagedThreadId}; Name:{Thread.CurrentThread.Name}; From thread pool? - {Thread.CurrentThread.IsThreadPoolThread}");
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
    }

    class ConsoleSynchronizationContext: SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            Thread.CurrentThread.Name = "NAME_THREAD";
            ThreadPool.QueueUserWorkItem(d);
            //base.Post(d, state);
        }
    }
}
