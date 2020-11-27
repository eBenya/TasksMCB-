﻿using System;
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
        }

        static void Main(string[] args)
        {            
            MainAsync();

            Console.ReadKey();
        }

        static async Task MainAsync()
        {
            Console.WriteLine($"ContextSTART: {SynchronizationContext.Current}");
            PrintInfo("Main Start", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread);
            SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());

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
