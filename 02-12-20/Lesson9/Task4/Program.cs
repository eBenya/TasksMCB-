using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 4
 *Создайте приложение по шаблону Console Application.
 *Используя Parallel LINQ (PLINQ)прочитайте из файла все слова,
 *которые начинаются на букву «a-A».
 *Выведите эти элементы на экран консоли.
 *Файл находится в папке с материалами. 
 *Название файла «data.txt».
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {            
            string path = "G:\\data.txt";

            Console.WriteLine($"File data.txt is exsist - {File.Exists(path)}\nWord when start with 'A' or 'a'");

            IEnumerable<object> res = null;

            using (StreamReader sr = new StreamReader(path))
            {
                var strArr = sr.ReadToEnd().Split(' ', '.', ',', '\n');

                res = from str in strArr.AsParallel()
                          where str.StartsWith("A", true, CultureInfo.CurrentCulture)
                          select str;
            }
            
            foreach (var item in res)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
