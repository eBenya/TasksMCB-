using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * # Задание 3 
     * Создайте проект по шаблону "ConsoleApplication".
     * Создайте метод с именем «private static double FindLastFibonacciNumber(int number)».
     * Метод должен найти и вернуть последнее число из последовательности Фибоначчи.
     * Для нахождения последнего числа из последовательности Фибоначчи в тело метода вставить следующий код:
     * 
     * Func<int, double> fib = null;
     * fib = (x) => x > 1 ? fib(x -1) + fib(x -2) : x;
     * return fib.Invoke(number);
     * 
     * Даже, если вы считаете, что этот код недостаточно оптимизирован, все равно нужно использовать его. 
     * В этом и смысл, что с помощью такого решения, последовательность числа Фибоначчи будет находится намного дольше
     * и с более сильной затратой ресурсов. 
     * Поэтому, вам нужно вызвать из метода Main этот метод в контексте задачи. 
     * Но так как эта операция займет много времени, вам нужно использовать флаг TaskCreationOptions.LongRunning, 
     * чтобы задача выполнялась в контексте потокавыполнения Thread и не занимала потоки из пула. 
     * Результат асинхронной задачи необходимо вывести на экран консоли. 
     * Сделайте это с помощью продолжения.
     **/
    class Program
    {
        static void Main(string[] args)
        {
            int num = 7;
            Task<double> task = new Task<double>(() => FindLastFibonacciNumber(num), TaskCreationOptions.LongRunning);
            task.Start();

            task.ContinueWith((t) => Console.WriteLine(t.Result));

            Console.ReadKey();
        }
        private static double FindLastFibonacciNumber(int number)
        {
            Func<int, double> fib = null;
            fib = (x) => x > 1 ? fib(x - 1) + fib(x - 2) : x;
            return fib.Invoke(number);
        }
    }
}