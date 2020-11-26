using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * # Задание 2 
     * Перепишите предыдущий пример, используя асинхронный метод Main. 
     * При вызове метода CalculateFactorialAsync дождитесь завершения асинхронного метода.
     * Для этого используйте оператор await.
     **/
    class Program
    {
        private static int TIMER = 50;
        private static async void Main(string[] args)
        {
            Random r = new Random();
            
            await CalculateFactorialAsync(15);

            for (int i = 0; i < TIMER; i++)
            {
                Thread.Sleep(10);
                Console.Write($"{(char)r.Next(30, 50)}");
            }
            Console.ReadKey();
        }

        private static async Task CalculateFactorialAsync(int num)
        {
            int res = await Task.Run(() =>
            {
                Thread.Sleep(TIMER*10);
                return CalculateFactorial(num);
            });
            Console.WriteLine($"\n\nFactorial = {res}\n\n");
        }
        private static int CalculateFactorial(int num)
        {
            int res = 1;
            for (int i = 1; i <= num; i++)
            {
                res *= i;
            }
            return res;
        }
    }
}