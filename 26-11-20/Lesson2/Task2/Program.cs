using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /**
     * # Задание 2
     *   Создайте  проект  по  шаблону  "Console  Application".  
     *   Создайтеметод«private static void WriteChar(object symbol) { }».
     *   В теле метода создайте цикл for, размерностью 160 итераций,
     *   который в своем теле с задержкой в пол секунды 
     *   выводит на экран консоли значение параметра symbol, 
     *   приведенного  к  типу  char.  
     *   Вызовите  метод  WriteChar  из  метода  Main, 
     *   в  контексте  задачи, передавая,
     *   в качестве параметра значение "!".
     *   Все время, пока метод WriteChar выполняется, 
     *   из метода Main выводите на экран консоли "$".
     *   Когда задача закончит свое выполнение,
     *   выведите на экран консоли строку : 
     *   "Метод Main закончил свою работу".
     **/
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(()=>WriteChar('!'));

            WriteChar('$');

            Console.WriteLine("Метод Main закончил свою работу");
            Console.ReadKey();
        }
        private static void WriteChar(object symbol)
        {
            char c = (char)symbol;
            for (int i = 0; i < 160; i++)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
        }
    }
}