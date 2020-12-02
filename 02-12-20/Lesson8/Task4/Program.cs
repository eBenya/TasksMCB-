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
 *Задание 4
 *Создайте приложение по шаблону Console Application.
 *Используя параллельный цикл ForEach прочитайте содержимое файла.
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
            Console.WriteLine($"File data.txt is exsist - {File.Exists("G:\\data.txt")}");
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    Parallel.ForEach(line, (i) => Console.Write(i));
                }
            }
        }
    }    
}
