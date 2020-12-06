using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 3
 *Создайте приложение по шаблону Console Application. 
 *Создайте массив целочисленных элементов размерностью в 1000. 
 *Проинициализируйте массив с помощью параллельного цикла For 
 *от 0 до максимального размера. 
 *Используя Parallel LINQ (PLINQ) выберите все нечетные элементы,
 *сохраняя исходный порядок последовательности.
 *Выведите эти элементы на экран консоли.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[1000];
            Parallel.For(0, arr.Length, i => arr[i] = i);
            var res = from num in arr.AsParallel().AsOrdered()
                      where num % 2 == 0
                      select num;
            foreach (var item in res)
            {
                Console.Write($"{item}\t");
            }
        }
    }
}
