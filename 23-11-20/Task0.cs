using System;
using System.Runtime.InteropServices;
using System.Threading;


/**
 * Создайте  проект  по  шаблону  "ConsoleApplication".  
 * Создайте  метод  с  названием  WriteChar, 
 * который принимает один параметр типа char с названием symbol.
 * В методе необходимо создать цикл for размерностью в 160 итераций,
 * который будет выводить на экран консоли значение параметра  symbol
 * с  задержкой  в  100  миллисекунд.  Из  метода  Main,
 * используя  пул  потоков, организуйте параллельный вывод на экран двух символов
 * звездочки и знака восклицания.
 **/

namespace ConsoleApp1
{
    class Program
    {
        const int sleep = 50;
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(WriteChar, '*');
            ThreadPool.QueueUserWorkItem(WriteChar, '!');

            Console.Read();
        }

        public static void WriteChar(object symbol)
        {
            char c = (char)symbol;

            for (int i = 1; i < 160; i++)
            {
                Console.Write(c);
                Thread.Sleep(sleep);
            }
        }
    }
}