using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 5
 *Создайте приложение по шаблону Console  Application. 
 *Запросите у пользователя любое число. 
 *Создайте асинхронную операцию с помощью класса TaskCompletionSource, 
 *которая в контексте потока из пула, 
 *считает всю последовательность чисел от 0 до указанного пользователем числа. 
 *Результат задачи выведите на экран консоли.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            CountToAsync(15);
            Console.ReadKey();
        }

        private static Task CountToAsync(int num)
        {
            TaskCompletionSource<int> task = new TaskCompletionSource<int>();
            Task.Run(async () =>
            {
                for (int i = 1; i <= num; i++)
                {
                    Console.WriteLine(i);
                    await Task.Delay(100);
                }                
            });

            return task.Task;
        }
    }
}
