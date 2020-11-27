using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 2
 *Создайте приложение по шаблону Console Application. 
 *Создайте свой контекст синхронизации и переопределите метод Post. 
 *Метод Post должен выполнять полученный делегат в контексте потока (экземпляр класса Thread).
 *Дайте потоку, созданному для выполнения делегата в методе Post, имя.
 *Установите в начале работы метода Main созданный контекст синхронизации.
 *Создайте асинхронный метод, который в контексте задачи считает факториал числа через цикл.
 *Используйте оператор await для ожидания завершения этой задачи.
 *До и после оператора await выведите на экран консоли
 *  в каком id потока (ManagedThreadId) выполняется эта часть, 
 *  имя у потока (Name) и
 *  является ли поток потоком из пула (IsThreadPoolThread).
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
