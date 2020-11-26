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
     * Создайте метод «private static int[] SortArray(bool isAscending, params int[] array)».
     * Метод должен отсортировать массив и вернуть отсортированный массив в виде результата. 
     * Если параметр isAscendingравен true-сортировать по возрастанию, 
     * если false-сортировать по убыванию. 
     * Как организовать алгоритм сортировки, полностью зависит от вашего выбора.
     * Вызвать метод SortArray в контексте задачи для большого массива типа int.
     * Результат задачи обработать в продолжении, где нужно вывестина экран консоли 
     * все элементы массива через запятую.
     **/
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 5, 4, 6, 7, 63, 7, 4, 2, 7, 4, 1, 7, 1, 2 };

            foreach (var item in arr)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine("\nSorting...");
            SortArray(true, arr);
            foreach (var item in arr)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine("\nSorting...");
            SortArray(false, arr);
            foreach (var item in arr)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine("\nМетод Main закончил свою работу");
            Console.ReadKey();
        }
        private static int[] SortArray(bool isAscending, params int[] array)
        {
            if (isAscending)
            {
               Array.Sort(array, (x, y) => x.CompareTo(y));
            }
            else
            {
                Array.Sort(array, (x, y) => y.CompareTo(x));
            }
            return array;
        }
    }
}