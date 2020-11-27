using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        const int sleep = 50;
        static void Main(string[] args)
        {
            Console.WriteLine($"Main start, thread id - {Thread.CurrentThread.ManagedThreadId}");

            WriteCharAsync('*');
            WriteChar('+');

            Console.WriteLine($"Main end, thread id - {Thread.CurrentThread.ManagedThreadId}");

            Console.ReadKey();
        }

        static async void WriteCharAsync(char c)
        {
            Console.WriteLine($"WriteCharAsync start, thread id - {Thread.CurrentThread.ManagedThreadId}");

            //Run hard task
            //await Task.Run(()=> Thread.Sleep(5000)); 

            await Task.Run(()=>WriteChar(c));

            Console.WriteLine($"WriteCharAsync start, thread id - {Thread.CurrentThread.ManagedThreadId}");
        }
        static void WriteChar(char c)
        {
            Console.WriteLine($"Thread id - {Thread.CurrentThread.ManagedThreadId}. Task id - {Task.CurrentId}");

            for (int i = 0; i < 60; i++)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }

        }
    }
}