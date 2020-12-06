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
 *Создайте массивцелочисленныхэлементов,размерностью в 10000000. 
 *Проинициализируйте массив с помощью параллельного цикла For от 0 до максимального размера. 
 *Создайте потокобезопасную коллекцию на свое усмотрение. 
 *Используя параллельный цикл ForEach переберите элементы массива
 *и добавляйте в потокобезопаснуюколлекцию только те элементы, 
 *которые являются степенью двойки. 
 *Выведите на экран консоли элементы из вашей потокобезопасной коллекции.
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10_000_000];
            Parallel.For(0, arr.Length, i => arr[i] = i);

            ConcurrentQueue<int> cQueue = new ConcurrentQueue<int>();

            Action<int> part = (item) =>
            {
                if (item != 0 && (item & (item - 1)) == 0)
                {
                    cQueue.Enqueue(item);
                }
            };

            Parallel.ForEach(arr, part);

            Parallel.ForEach(cQueue, (item) => Console.WriteLine(item));
        }
    }
}
