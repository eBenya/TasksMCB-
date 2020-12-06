using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 *Задание 2
 *Создайте приложение по шаблону Console Application. 
 *Создайте асинхронный метод WriteToFileAsync, 
 *который в асинхронном режиме производит запись в файл. 
 *Организуйте ввод сообщений с клавиатуры в консоли. 
 *Результатввода данных пользователем должен быть записан в файл 
 *с помощью вашего асинхронного метода WriteToFileAsync.
 **/

namespace ConsoleTEST
{
    class Program
    {
        private static string path = "G:\\File.txt";
        static void Main(string[] args)
        {
            while (true)
            {
                string str = Console.ReadLine();
                if (str == "exit" || str.Length > 4095)
                {
                    break;
                }
                WriteToAsync($"{str}\n");
            }
        }

        private static async void WriteToAsync(string str)
        {
            byte[] buff = Encoding.UTF8.GetBytes(str); //new byte[4096];
            FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write,
                FileShare.ReadWrite, buff.Length, FileOptions.Asynchronous);
            await Task.Run(() => fileStream.Write(buff, 0, buff.Length));
        }

        public static void PrintInfo(string text, int ManagedID, string threadName, bool isThreadPoolThread)
        {
            Console.WriteLine($"{text} ID:{ManagedID}; Name:{threadName}; From thread pool - {isThreadPoolThread}");
        }
    }
}
