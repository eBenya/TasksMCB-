using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    internal class Program
    {
        private static void Main()
        {
            int number = 13;

            var result = CalculatyFactorialAsync(number);

            result.ContinueWith((t) => Console.WriteLine($"Результат{t.Result}"));

            while (true)
            {
                Console.Write("*");
                Thread.Sleep(300);
            }
        }

        private static Task<long> CalculatyFactorialAsync(int num)
        {
            return Task.Run(() => CalculateFactorial(num));
        }

        private static long CalculateFactorial(int number)
        {
            Thread.Sleep(500);

            if (number == 1)
            {
                return number;
            }
            else
            {
                return CalculateFactorial(number - 1) * number;
            }
        }
    }
}
