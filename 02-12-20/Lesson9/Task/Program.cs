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
 *Задание 
 *Создайте приложение по шаблону ConsoleApplication.
 *Создайте массив целочисленных элементов размерностью в 10000000. 
 *Проинициализируйте массив с помощью параллельного цикла For 
 *от 0 до максимального размера. 
 *Используя Parallel LINQ (PLINQ) выберите все элементы, 
 *которые являются степенью двойки. 
 *Выведите эти элементы на экран консоли
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[10_000_000];
            Parallel.For(0, arr.Length, (i) => arr[i] = i);

            var res = from item in arr.AsParallel()
                      where item != 0 && (item & (item - 1)) == 0
                      select item;
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}
