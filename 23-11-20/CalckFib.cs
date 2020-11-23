using System;
using System.Threading;

namespace AsyncProgramming
{
    internal class Program
    {
        private static void Main()
        {
            int number = 13;

            long result = CalculateFactorial(number);

            Console.WriteLine($"Результат - {result}");

            while (true)
            {
                Console.Write("*");
                Thread.Sleep(300);
            }
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
