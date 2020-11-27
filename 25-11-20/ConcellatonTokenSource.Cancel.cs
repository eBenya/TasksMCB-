using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            int num = 6;
            Task task1 = new Task(() =>
            {
                int res = 1;
                for (int i = 1; i <= num; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Operaton is denied");
                        return;
                    }
                    res *= i;
                    Console.WriteLine($"Factorial of a number: {res}");
                    Thread.Sleep(3000);
                }
            });

            task1.Start();

            Console.WriteLine("Enter Y for canceled operaton");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                cancellationTokenSource.Cancel();
            }
            Console.ReadKey();
        }
    }
}



using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * cancellationTokenSource.Cancel();
     * Уронит всю задачу в момент вызова метода Cancel();
     **/
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            int num = 6;
            Task task1 = new Task(() => Factorial(num, token));

            task1.Start();

            Console.WriteLine("Enter Y for canceled operaton");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                cancellationTokenSource.Cancel();
            }
            Console.ReadKey();
        }

        static void Factorial(int num, CancellationToken token)
        {
            int res = 1;
            for (int i = 1; i <= num; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Operaton is denied");
                    return;
                }
                res *= i;
                Console.WriteLine($"Factorial of a number: {res}");
                Thread.Sleep(3000);
            }
        }
    }
}